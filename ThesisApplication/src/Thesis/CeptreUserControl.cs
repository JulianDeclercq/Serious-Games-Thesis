using System;
using System.Collections.Generic;
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
        private string _origSsLbl;
        private WordNetSimilarityModel _semanticSimilarityModel;
        private DisplayForm _displayForm;

        public CeptreUserControl()
        {
            InitializeComponent();

            // create wordnet engine (use disk-based retrieval by default)
            string path = Path.GetFullPath(@"..\..\..\..\resources\");
            _wordNetEngine = new WordNetEngine(path, false);

            if (!_wordNetEngine.InMemory)
                test.Text += " (will take a while)";

            // populate POS list
            foreach (WordNetEngine.POS p in Enum.GetValues(typeof(WordNetEngine.POS)))
                if (p != WordNetEngine.POS.None)
                    pos.Items.Add(p);

            pos.SelectedIndex = 0;

            // allow scrolling of synset list
            synSets.HorizontalScrollbar = true;

            _semSimSs1 = _semSimSs2 = null;
            _origSsLbl = ss1.Text;
            _semanticSimilarityModel = new WordNetSimilarityModel(_wordNetEngine);
        }

        public void SetDisplayFormReference(DisplayForm reference)
        {
            // Set the reference
            _displayForm = reference;
        }

        private void getSynSets_Click(object sender, EventArgs e)
        {
            synSets.Items.Clear();

            // retrieve synsets
            Set<SynSet> synSetsToShow = null;
            if (synsetID.Text != "")
            {
                try { synSetsToShow = new Set<SynSet>(new SynSet[] { _wordNetEngine.GetSynSet(synsetID.Text) }); }
                catch (Exception)
                {
                    MessageBox.Show("Invalid SynSet ID");
                    return;
                }
            }
            else
            {
                // get single most common synset
                if (mostCommon.Checked)
                {
                    try
                    {
                        SynSet synset = _wordNetEngine.GetMostCommonSynSet(word.Text, (WordNetEngine.POS)pos.SelectedItem);

                        if (synset != null)
                            synSetsToShow = new Set<SynSet>(new SynSet[] { synset });
                    }
                    catch (Exception ex) { MessageBox.Show("Error:  " + ex); return; }
                }
                // get all synsets
                else
                {
                    try { synSetsToShow = _wordNetEngine.GetSynSets(word.Text, (WordNetEngine.POS)pos.SelectedItem); }
                    catch (Exception ex) { MessageBox.Show("Error:  " + ex); return; }
                }
            }

            if (synSetsToShow.Count > 0)
                foreach (SynSet synSet in synSetsToShow)
                    synSets.Items.Add(synSet);
            else
                MessageBox.Show("No synsets found");
        }

        private void word_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                getSynSets_Click(sender, e);
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

        private void word_TextChanged(object sender, EventArgs e)
        {
            synsetID.TextChanged -= new EventHandler(synsetID_TextChanged);
            synsetID.Text = "";
            synsetID.TextChanged += new EventHandler(synsetID_TextChanged);
        }

        private void synsetID_TextChanged(object sender, EventArgs e)
        {
            word.TextChanged -= new EventHandler(word_TextChanged);
            word.Text = "";
            word.TextChanged += new EventHandler(word_TextChanged);
        }

        private void synsetID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                getSynSets_Click(sender, e);
        }

        private void computeSemSim_Click(object sender, EventArgs e)
        {
            if (_semSimSs1.POS != _semSimSs2.POS)
            {
                MessageBox.Show("Selected synsets must have the same part-of-speech.");
                return;
            }

            string result = "";
            foreach (WordNetSimilarityModel.Strategy strategy in Enum.GetValues(typeof(WordNetSimilarityModel.Strategy)))
            {
                float sim = _semanticSimilarityModel.GetSimilarity(_semSimSs1, _semSimSs2, strategy, WordNetEngine.SynSetRelation.Hypernym);
                result += strategy + ":  " + sim + Environment.NewLine;
            }

            MessageBox.Show(result);
        }

        private void synSets_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (synSets.SelectedItem == null)
                return;

            SynSet s = (SynSet)synSets.SelectedItem;
            if (_semSimSs1 == null)
            {
                _semSimSs1 = s;
                ss1.Text = _semSimSs1.ToString() + " (double-click to remove)";
            }
            else if (_semSimSs2 == null)
            {
                _semSimSs2 = s;
                ss2.Text = _semSimSs2.ToString() + " (double-click to remove)";
            }
            else
                MessageBox.Show("Please remove one of the synsets selected below (double-click it)");

            computeSemSim.Enabled = _semSimSs1 != null && _semSimSs2 != null;
        }

        private void ss1_DoubleClick(object sender, EventArgs e)
        {
            ss1.Text = _origSsLbl;
            _semSimSs1 = null;
            computeSemSim.Enabled = false;
        }

        private void ss2_DoubleClick(object sender, EventArgs e)
        {
            ss2.Text = _origSsLbl;
            _semSimSs2 = null;
            computeSemSim.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _displayForm.UpdateDisplayMode(DisplayForm.DisplayMode.WordNet);
        }
    }
}