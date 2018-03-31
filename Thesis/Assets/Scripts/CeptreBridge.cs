using System;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.IO;

public class CeptreBridge : MonoBehaviour
{
    public Text testText;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //StartCeptre("hello-world");
            StartCeptre("hello-world-interactive");
        }
    }

    private void StartCeptre(string ceptreFile)
    {
        // Append the ceptre extension if it wasn't present in the passed filename
        string ceptreExtension = ".cep";
        if (!ceptreFile.EndsWith(ceptreExtension))
            ceptreFile += ceptreExtension;

        // Specify path names
        string ceptreFolder = "Ceptre";
        string ceptreFilesFolder = "files";
        string executableName = "ceptre.exe";

        // Specify the process
        Process ceptreProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = string.Format("{0}/{1}/{2}", Application.streamingAssetsPath, ceptreFolder, executableName),
                Arguments = string.Format("{0}/{1}/{2}/{3}", Application.streamingAssetsPath, ceptreFolder, ceptreFilesFolder, ceptreFile),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };

        // Start the process
        ceptreProcess.Start();

        // Read the output from the process
        string output = "";

        // Read until the end of the stream (== end of Ceptre process)
        while (!ceptreProcess.StandardOutput.EndOfStream)
        {
            // For formatting purposes
            bool providedInput = false;

            // Peek for a question mark. If a question mark is provided in the Ceptre output, input is expected
            int peek = ceptreProcess.StandardOutput.Peek();
            if ((char)peek == '?')
            {
                // Input choice one
                string choice = "1";
                ceptreProcess.StandardInput.WriteLine(choice);

                // Toggle bool
                providedInput = true;
            }

            // Read the line from the program
            string line = ceptreProcess.StandardOutput.ReadLine();

            // Add endlines for formatting (add one extra if input has been provided to match the original ceptre from the console)
            line += (providedInput) ? "\n\n" : "\n";

            // Add the line to the output, print choice if it is not empty
            output += line;
        }

        // Update the text box
        testText.text = output;

        // Print the output
        print(output);
    }
}