using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Threading;

using LAIR.ResourceAPIs.WordNet;
using LAIR.Collections.Generic;

namespace Thesis
{
    public partial class CeptreUserControl : UserControl
    {
        private WordNetEngine _wordNetEngine;
        private DisplayForm _displayForm;
        private Process _ceptreProcess = null;

        public CeptreUserControl()
        {
            InitializeComponent();

            // create wordnet engine (use disk-based retrieval by default)
            string path = Path.GetFullPath(@"..\..\..\..\resources\");
            _wordNetEngine = new WordNetEngine(path, false);
        }

        public void SetDisplayFormReference(DisplayForm reference)
        {
            // Set the reference
            _displayForm = reference;
        }

        private void test_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(delegate ()
            {
                Invoke(new MethodInvoker(delegate () { Enabled = false; }));

                // test all words
                Dictionary<WordNetEngine.POS, Set<string>> words = _wordNetEngine.AllWords;
                foreach (WordNetEngine.POS pos in words.Keys)
                    foreach (string word in words[pos])
                    {
                        // get synsets
                        Set<SynSet> synsets = _wordNetEngine.GetSynSets(word, pos);
                        if (synsets.Count == 0)
                            if (
                                MessageBox.Show("Failed to get synset for " + pos + ":  " + word + ". Quit?", "Quit?",
                                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                                return;

                        // make sure there's a most common synset
                        if (_wordNetEngine.GetMostCommonSynSet(word, pos) == null)
                            throw new NullReferenceException("Failed to find most common synset");

                        // check each synset
                        foreach (SynSet synset in synsets)
                        {
                            // check lexically related words
                            synset.GetLexicallyRelatedWords();

                            // check relations
                            foreach (WordNetEngine.SynSetRelation relation in synset.SemanticRelations)
                                synset.GetRelatedSynSets(relation, false);

                            // check lex file name
                            if (synset.LexicographerFileName == WordNetEngine.LexicographerFileName.None)
                                throw new Exception("Invalid lex file name");
                        }
                    }

                MessageBox.Show("Test completed. Everything looks okay.");

                Invoke(new MethodInvoker(delegate () { Enabled = true; }));
            }));

            t.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _displayForm.UpdateDisplayMode(DisplayForm.DisplayMode.WordNet);
        }

        private void btnStartCeptre_Click(object sender, EventArgs e)
        {
            // Start ceptre
            StartCeptre("cur.cep");
        }

        private void btnShowGraph_Click(object sender, EventArgs e)
        {
            // Show the generated graph
            // Specify the process
            // For some obscure reason, just running cmd.exe with the path as commandline argument didn't work..
            Process process = new Process
            {
                StartInfo =
                {
                    Verb = "runas", // run as administrator
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            // Start the process
            process.Start();

            // Open the generated file
            string graphPath = $@"{GraphLoader.GenerationPath()}\{GraphLoader.LatestGeneratedFileName()}";
            process.StandardInput.WriteLine(graphPath);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // Confirm the user selection box and send it to the ceptre program
            SendUserInput(comboBoxUserInput.SelectedItem.ToString());
        }

        private void btnExportLogAndGraph_Click(object sender, EventArgs e)
        {
            // Show a save file dialog
            string resourcesFolder = Path.GetFullPath(@"..\..\resources");

            // Avoid using the annoying folder dialog by creating a dummy file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = $@"{resourcesFolder}\InterestingRuns"
            };

            // Show the dialog
            var result = saveFileDialog.ShowDialog();

            // Check if the result was ok, otherwise leave the method
            if (result != DialogResult.OK)
                return;

            // Create a directory
            string directoryName = Path.GetDirectoryName(saveFileDialog.FileName);
            string fileName = Path.GetFileName(saveFileDialog.FileName);
            string savePath = $@"{directoryName}\{fileName}_{DateTime.UtcNow:yyyy_MM_dd__HH_mm_ss}";
            Directory.CreateDirectory(savePath);

            // Copy the graph
            string origin = $@"{resourcesFolder}\{GraphLoader.LatestGeneratedFileName()}";
            string destination = $@"{savePath}\{Path.GetFileName(saveFileDialog.FileName)}.png";
            File.Copy(origin, destination);

            // Copy the log
            origin = $@"{resourcesFolder}\Ceptre\log.txt";
            destination = $@"{savePath}\{Path.GetFileName(saveFileDialog.FileName)}.txt";
            File.Copy(origin, destination);
        }

        #region Ceptre control

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
                btnShowGraph.Enabled = true;
            }
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
                    comboBoxUserInput.Items.Clear();

                    // Safely retrieve the last N elements from the list (max is to avoid negative numbers at all cost)
                    foreach (string choice in lines.Skip(Math.Max(0, lines.Count() - inputChoices)))
                        comboBoxUserInput.Items.Add(choice);

                    // Highlight the first choice
                    comboBoxUserInput.SelectedIndex = 0;

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

        #endregion
    }
}