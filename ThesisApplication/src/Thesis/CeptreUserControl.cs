using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using LAIR.ResourceAPIs.WordNet;
using LAIR.Collections.Generic;

namespace Thesis
{
    public partial class CeptreUserControl : UserControl
    {
        private WordNetEngine _wordNetEngine;
        private SynSet _semSimSs1;
        private SynSet _semSimSs2;
        private WordNetSimilarityModel _semanticSimilarityModel;
        private DisplayForm _displayForm;
        private CeptreBridge _ceptreBridge = null;

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
                                if (MessageBox.Show("Failed to get synset for " + pos + ":  " + word + ". Quit?", "Quit?", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            if (_ceptreBridge == null)
            {
                _ceptreBridge = new CeptreBridge();
                _ceptreBridge.SetUserInputCombobox(comboBoxUserInput);
            }

            _ceptreBridge.StartCeptre("cur.cep");
        }

        private void btnGenerateGraph_Click(object sender, EventArgs e)
        {
            string outputFileName = "graphOutput.png";

            // Generate the graph
            GraphLoader.GenerateGraph(outputFileName);

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
            process.StandardInput.WriteLine($@"{GraphLoader.GenerationPath()}\{outputFileName}");
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            // Confirm the user selection box and send it to the ceptre program
            _ceptreBridge?.SendUserInput(comboBoxUserInput.SelectedItem.ToString());
        }
    }
}