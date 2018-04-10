using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

public class CeptreBridge
{
    private ComboBox _userInputComboBox = null;

    private Process _ceptreProcess = null;

    public void SetUserInputCombobox(ComboBox userInputComboBox)
    {
        _userInputComboBox = userInputComboBox;
    }

    public void StartCeptre(string ceptreFile, string ceptreFolder = "files", bool generateGraph = true)
    {
        // Kill the ceptre process if it already existed
        if (_ceptreProcess != null && !_ceptreProcess.HasExited)
            _ceptreProcess.Kill();

        // Append the ceptre extension if it wasn't present in the passed filename
        string ceptreExtension = ".cep";
        if (!ceptreFile.EndsWith(ceptreExtension))
            ceptreFile += ceptreExtension;

        // Specify the process
        string resourcesFolder = Path.GetFullPath(@"..\..\resources");
        string ceptrePath = $"{resourcesFolder}/Ceptre";

        _ceptreProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                Verb = "runas", // run as administrator
                FileName = $@"{ceptrePath}\ceptre.exe",
                //Arguments = string.Format(@"{0}\{1}\{2}", ceptrePath, ceptreFolder, ceptreFile),
                Arguments = $@"{ceptreFolder}\{ceptreFile}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = ceptrePath  // When UseShellExecute is false, gets or sets the working directory FOR the process to be started.
                                               // When UseShellExecute is true, gets or sets the directory that CONTAINS the process to be started.
            }
        };

        // Start the process
        _ceptreProcess.Start();

        // Interpret the input and output streams
        InterpretStreams();

        // Generate the graph
        if (generateGraph)
        {
            // Generate the graph
            GraphLoader.GenerateGraph("graphOutput.png");

            // Enable the show graph button
        }
    }

    private void InterpretStreams()
    {
        // Read the output from the process
        string output = "";
        List<string> lines = new List<string>();

        // Read until the end of the stream (== end of Ceptre process)
        while (!_ceptreProcess.StandardOutput.EndOfStream)
        {
            // Peek for a question mark. If a question mark is provided in the Ceptre output, input is expected
            int peek = _ceptreProcess.StandardOutput.Peek();
            if ((char)peek == '?')
            {
                // Check last line for amount of input
                string lastLine = lines.Last();
                int inputChoices = int.Parse(lastLine.Substring(0, lastLine.IndexOf(":")));

                // Clear all previous choices
                _userInputComboBox.Items.Clear();

                // Safely retrieve the last N elements from the list (max is to avoid negative numbers at all cost)
                foreach (string choice in lines.Skip(Math.Max(0, lines.Count() - inputChoices)))
                    _userInputComboBox.Items.Add(choice);

                // Highlight the first choice
                _userInputComboBox.SelectedIndex = 0;

                // Exit the method, wait for a choice to be made
                return;
            }

            // Read the line from the program
            string line = _ceptreProcess.StandardOutput.ReadLine();

            // Add endlines
            line += Environment.NewLine;

            // Add the line to the output, print choice if it is not empty
            output += line;
            lines.Add(line);
        }

        if (_ceptreProcess.HasExited)
            Console.WriteLine("Ceptre finished running.");

        // Print the output
        foreach (string line in lines)
            Console.WriteLine(line);
    }

    public void SendUserInput(string userInput)
    {
        // Filter the userinput to only allow the number
        int inputChoice = int.Parse(userInput.Substring(0, userInput.IndexOf(":")));

        // Send the input to the stream
        _ceptreProcess.StandardInput.WriteLine(inputChoice);

        // Consume the line so the stream doesn't choke
        string lastLine = _ceptreProcess.StandardOutput.ReadLine();

        // Restart the interpretation of the stream
        InterpretStreams();
    }
}