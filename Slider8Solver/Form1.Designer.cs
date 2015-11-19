namespace Slider8Solver
{
	partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanelSolver = new System.Windows.Forms.TableLayoutPanel();
            this.labelAStarResult = new System.Windows.Forms.Label();
            this.labelDepthFirstResult = new System.Windows.Forms.Label();
            this.buttonSearchBreadthFirst = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxPuzzleSize = new System.Windows.Forms.ComboBox();
            this.textBoxPuzzleInput2 = new System.Windows.Forms.TextBox();
            this.buttonSearchDepthFirst = new System.Windows.Forms.Button();
            this.buttonSearchAStar = new System.Windows.Forms.Button();
            this.buttonRandomizePuzzle2 = new System.Windows.Forms.Button();
            this.labelBreadtFirstResult = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanelPuzzle = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanelSolver.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelSolver
            // 
            this.tableLayoutPanelSolver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanelSolver.ColumnCount = 2;
            this.tableLayoutPanelSolver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanelSolver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanelSolver.Controls.Add(this.labelAStarResult, 1, 2);
            this.tableLayoutPanelSolver.Controls.Add(this.labelDepthFirstResult, 1, 1);
            this.tableLayoutPanelSolver.Controls.Add(this.buttonSearchBreadthFirst, 0, 0);
            this.tableLayoutPanelSolver.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanelSolver.Controls.Add(this.comboBoxPuzzleSize, 1, 4);
            this.tableLayoutPanelSolver.Controls.Add(this.textBoxPuzzleInput2, 1, 3);
            this.tableLayoutPanelSolver.Controls.Add(this.buttonSearchDepthFirst, 0, 1);
            this.tableLayoutPanelSolver.Controls.Add(this.buttonSearchAStar, 0, 2);
            this.tableLayoutPanelSolver.Controls.Add(this.buttonRandomizePuzzle2, 0, 3);
            this.tableLayoutPanelSolver.Controls.Add(this.labelBreadtFirstResult, 1, 0);
            this.tableLayoutPanelSolver.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelSolver.Name = "tableLayoutPanelSolver";
            this.tableLayoutPanelSolver.RowCount = 5;
            this.tableLayoutPanelSolver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelSolver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelSolver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelSolver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelSolver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelSolver.Size = new System.Drawing.Size(250, 434);
            this.tableLayoutPanelSolver.TabIndex = 2;
            // 
            // labelAStarResult
            // 
            this.labelAStarResult.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelAStarResult.AutoSize = true;
            this.labelAStarResult.Location = new System.Drawing.Point(168, 208);
            this.labelAStarResult.Name = "labelAStarResult";
            this.labelAStarResult.Size = new System.Drawing.Size(0, 13);
            this.labelAStarResult.TabIndex = 11;
            // 
            // labelDepthFirstResult
            // 
            this.labelDepthFirstResult.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelDepthFirstResult.AutoSize = true;
            this.labelDepthFirstResult.Location = new System.Drawing.Point(168, 122);
            this.labelDepthFirstResult.Name = "labelDepthFirstResult";
            this.labelDepthFirstResult.Size = new System.Drawing.Size(0, 13);
            this.labelDepthFirstResult.TabIndex = 10;
            // 
            // buttonSearchBreadthFirst
            // 
            this.buttonSearchBreadthFirst.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSearchBreadthFirst.AutoSize = true;
            this.buttonSearchBreadthFirst.Location = new System.Drawing.Point(5, 31);
            this.buttonSearchBreadthFirst.Name = "buttonSearchBreadthFirst";
            this.buttonSearchBreadthFirst.Size = new System.Drawing.Size(76, 23);
            this.buttonSearchBreadthFirst.TabIndex = 0;
            this.buttonSearchBreadthFirst.Text = "Breadth First";
            this.buttonSearchBreadthFirst.UseVisualStyleBackColor = true;
            this.buttonSearchBreadthFirst.Click += new System.EventHandler(this.buttonSearchBreadthFirst_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 376);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Number of rows:";
            // 
            // comboBoxPuzzleSize
            // 
            this.comboBoxPuzzleSize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxPuzzleSize.Enabled = false;
            this.comboBoxPuzzleSize.FormattingEnabled = true;
            this.comboBoxPuzzleSize.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.comboBoxPuzzleSize.Location = new System.Drawing.Point(108, 378);
            this.comboBoxPuzzleSize.MaxDropDownItems = 2;
            this.comboBoxPuzzleSize.Name = "comboBoxPuzzleSize";
            this.comboBoxPuzzleSize.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPuzzleSize.TabIndex = 6;
            this.comboBoxPuzzleSize.Text = "3";
            this.toolTip1.SetToolTip(this.comboBoxPuzzleSize, "Select the size of the puzzle.");
            this.comboBoxPuzzleSize.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // textBoxPuzzleInput2
            // 
            this.textBoxPuzzleInput2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPuzzleInput2.Location = new System.Drawing.Point(108, 291);
            this.textBoxPuzzleInput2.Name = "textBoxPuzzleInput2";
            this.textBoxPuzzleInput2.Size = new System.Drawing.Size(121, 20);
            this.textBoxPuzzleInput2.TabIndex = 3;
            this.textBoxPuzzleInput2.Text = "7,8,3,4,1,5,6,0,2";
            this.toolTip1.SetToolTip(this.textBoxPuzzleInput2, "Enter a puzzle line by line, each value separated by commas.");
            this.textBoxPuzzleInput2.TextChanged += new System.EventHandler(this.textBoxPuzzleInput_TextChanged);
            this.textBoxPuzzleInput2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPuzzleInput_KeyPress);
            // 
            // buttonSearchDepthFirst
            // 
            this.buttonSearchDepthFirst.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSearchDepthFirst.Location = new System.Drawing.Point(6, 117);
            this.buttonSearchDepthFirst.Name = "buttonSearchDepthFirst";
            this.buttonSearchDepthFirst.Size = new System.Drawing.Size(75, 23);
            this.buttonSearchDepthFirst.TabIndex = 1;
            this.buttonSearchDepthFirst.Text = "Depth First";
            this.buttonSearchDepthFirst.UseVisualStyleBackColor = true;
            this.buttonSearchDepthFirst.Click += new System.EventHandler(this.buttonSearchDepthFirst_Click);
            // 
            // buttonSearchAStar
            // 
            this.buttonSearchAStar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSearchAStar.Location = new System.Drawing.Point(6, 203);
            this.buttonSearchAStar.Name = "buttonSearchAStar";
            this.buttonSearchAStar.Size = new System.Drawing.Size(75, 23);
            this.buttonSearchAStar.TabIndex = 7;
            this.buttonSearchAStar.Text = "A*";
            this.buttonSearchAStar.UseVisualStyleBackColor = true;
            this.buttonSearchAStar.Click += new System.EventHandler(this.buttonSearchAStar_Click);
            // 
            // buttonRandomizePuzzle2
            // 
            this.buttonRandomizePuzzle2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRandomizePuzzle2.AutoSize = true;
            this.buttonRandomizePuzzle2.Location = new System.Drawing.Point(3, 278);
            this.buttonRandomizePuzzle2.Name = "buttonRandomizePuzzle2";
            this.buttonRandomizePuzzle2.Size = new System.Drawing.Size(81, 46);
            this.buttonRandomizePuzzle2.TabIndex = 8;
            this.buttonRandomizePuzzle2.Text = "Random Puzzle";
            this.buttonRandomizePuzzle2.UseVisualStyleBackColor = true;
            this.buttonRandomizePuzzle2.Click += new System.EventHandler(this.buttonRandom_Click);
            // 
            // labelBreadtFirstResult
            // 
            this.labelBreadtFirstResult.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelBreadtFirstResult.AutoSize = true;
            this.labelBreadtFirstResult.Location = new System.Drawing.Point(168, 36);
            this.labelBreadtFirstResult.Name = "labelBreadtFirstResult";
            this.labelBreadtFirstResult.Size = new System.Drawing.Size(0, 13);
            this.labelBreadtFirstResult.TabIndex = 9;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // tableLayoutPanelPuzzle
            // 
            this.tableLayoutPanelPuzzle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelPuzzle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanelPuzzle.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanelPuzzle.ColumnCount = 1;
            this.tableLayoutPanelPuzzle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPuzzle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelPuzzle.Location = new System.Drawing.Point(268, 12);
            this.tableLayoutPanelPuzzle.Name = "tableLayoutPanelPuzzle";
            this.tableLayoutPanelPuzzle.RowCount = 1;
            this.tableLayoutPanelPuzzle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPuzzle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 317F));
            this.tableLayoutPanelPuzzle.Size = new System.Drawing.Size(320, 320);
            this.tableLayoutPanelPuzzle.TabIndex = 3;
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.Location = new System.Drawing.Point(268, 339);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.Size = new System.Drawing.Size(320, 83);
            this.richTextBoxOutput.TabIndex = 4;
            this.richTextBoxOutput.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 434);
            this.Controls.Add(this.richTextBoxOutput);
            this.Controls.Add(this.tableLayoutPanelPuzzle);
            this.Controls.Add(this.tableLayoutPanelSolver);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.tableLayoutPanelSolver.ResumeLayout(false);
            this.tableLayoutPanelSolver.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSolver;
        private System.Windows.Forms.Button buttonSearchBreadthFirst;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxPuzzleSize;
        private System.Windows.Forms.TextBox textBoxPuzzleInput2;
        private System.Windows.Forms.Button buttonSearchDepthFirst;
        private System.Windows.Forms.Button buttonSearchAStar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonRandomizePuzzle2;
        private System.Windows.Forms.Label labelAStarResult;
        private System.Windows.Forms.Label labelDepthFirstResult;
        private System.Windows.Forms.Label labelBreadtFirstResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPuzzle;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;
    }
}

