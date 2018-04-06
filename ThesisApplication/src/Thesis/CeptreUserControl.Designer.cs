namespace Thesis
{
    partial class CeptreUserControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.word = new System.Windows.Forms.TextBox();
            this.getSynSets = new System.Windows.Forms.Button();
            this.test = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.synSets = new System.Windows.Forms.ListBox();
            this.pos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mostCommon = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.synsetID = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.wordNetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.computeSemSim = new System.Windows.Forms.Button();
            this.ss1 = new System.Windows.Forms.Label();
            this.ss2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // word
            // 
            this.word.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.word.Location = new System.Drawing.Point(78, 43);
            this.word.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.word.Name = "word";
            this.word.Size = new System.Drawing.Size(198, 26);
            this.word.TabIndex = 0;
            this.word.TextChanged += new System.EventHandler(this.word_TextChanged);
            this.word.KeyDown += new System.Windows.Forms.KeyEventHandler(this.word_KeyDown);
            // 
            // getSynSets
            // 
            this.getSynSets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.getSynSets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getSynSets.Location = new System.Drawing.Point(723, 40);
            this.getSynSets.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.getSynSets.Name = "getSynSets";
            this.getSynSets.Size = new System.Drawing.Size(112, 35);
            this.getSynSets.TabIndex = 3;
            this.getSynSets.Text = "Get synsets";
            this.getSynSets.UseVisualStyleBackColor = true;
            this.getSynSets.Click += new System.EventHandler(this.getSynSets_Click);
            // 
            // test
            // 
            this.test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.test.Location = new System.Drawing.Point(1035, 620);
            this.test.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(378, 35);
            this.test.TabIndex = 2;
            this.test.Text = "Test WordNet engine";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Word:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // synSets
            // 
            this.synSets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.synSets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.synSets.FormattingEnabled = true;
            this.synSets.ItemHeight = 20;
            this.synSets.Location = new System.Drawing.Point(20, 83);
            this.synSets.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.synSets.Name = "synSets";
            this.synSets.Size = new System.Drawing.Size(964, 204);
            this.synSets.TabIndex = 4;
            this.synSets.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.synSets_MouseDoubleClick);
            // 
            // pos
            // 
            this.pos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pos.FormattingEnabled = true;
            this.pos.Location = new System.Drawing.Point(344, 43);
            this.pos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pos.Name = "pos";
            this.pos.Size = new System.Drawing.Size(90, 28);
            this.pos.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(286, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "POS:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.mostCommon);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.synsetID);
            this.groupBox1.Controls.Add(this.synSets);
            this.groupBox1.Controls.Add(this.word);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.getSynSets);
            this.groupBox1.Controls.Add(this.pos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(18, 66);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(1008, 332);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Synsets";
            // 
            // mostCommon
            // 
            this.mostCommon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mostCommon.AutoSize = true;
            this.mostCommon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mostCommon.Location = new System.Drawing.Point(847, 46);
            this.mostCommon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mostCommon.Name = "mostCommon";
            this.mostCommon.Size = new System.Drawing.Size(135, 24);
            this.mostCommon.TabIndex = 7;
            this.mostCommon.Text = "Most common";
            this.mostCommon.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(444, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "ID:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // synsetID
            // 
            this.synsetID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.synsetID.Location = new System.Drawing.Point(484, 43);
            this.synsetID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.synsetID.Name = "synsetID";
            this.synsetID.Size = new System.Drawing.Size(206, 26);
            this.synsetID.TabIndex = 5;
            this.synsetID.TextChanged += new System.EventHandler(this.synsetID_TextChanged);
            this.synsetID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.synsetID_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(1035, 66);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(376, 332);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Semantic relations";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wordNetToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1430, 35);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // wordNetToolStripMenuItem
            // 
            this.wordNetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.wordNetToolStripMenuItem.Name = "wordNetToolStripMenuItem";
            this.wordNetToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.wordNetToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 30);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(1036, 411);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(376, 200);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lexical relations";
            // 
            // computeSemSim
            // 
            this.computeSemSim.Enabled = false;
            this.computeSemSim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.computeSemSim.Location = new System.Drawing.Point(18, 134);
            this.computeSemSim.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.computeSemSim.Name = "computeSemSim";
            this.computeSemSim.Size = new System.Drawing.Size(285, 35);
            this.computeSemSim.TabIndex = 8;
            this.computeSemSim.Text = "Compute semantic similarity";
            this.computeSemSim.UseVisualStyleBackColor = true;
            this.computeSemSim.Click += new System.EventHandler(this.computeSemSim_Click);
            // 
            // ss1
            // 
            this.ss1.AutoSize = true;
            this.ss1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ss1.Location = new System.Drawing.Point(14, 62);
            this.ss1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ss1.Name = "ss1";
            this.ss1.Size = new System.Drawing.Size(207, 20);
            this.ss1.TabIndex = 9;
            this.ss1.Text = "Double-click synset to add";
            this.ss1.DoubleClick += new System.EventHandler(this.ss1_DoubleClick);
            // 
            // ss2
            // 
            this.ss2.AutoSize = true;
            this.ss2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ss2.Location = new System.Drawing.Point(14, 92);
            this.ss2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ss2.Name = "ss2";
            this.ss2.Size = new System.Drawing.Size(207, 20);
            this.ss2.TabIndex = 10;
            this.ss2.Text = "Double-click synset to add";
            this.ss2.DoubleClick += new System.EventHandler(this.ss2_DoubleClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.ss1);
            this.groupBox4.Controls.Add(this.ss2);
            this.groupBox4.Controls.Add(this.computeSemSim);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(20, 411);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(1008, 200);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Semantic similarity";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(361, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CeptreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.test);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1430, 724);
            this.Name = "CeptreForm";
            this.Size = new System.Drawing.Size(1430, 724);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox word;
        private System.Windows.Forms.Button getSynSets;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox synSets;
        private System.Windows.Forms.ComboBox pos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wordNetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox synsetID;
        private System.Windows.Forms.CheckBox mostCommon;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button computeSemSim;
        private System.Windows.Forms.Label ss2;
        private System.Windows.Forms.Label ss1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button1;
    }
}

