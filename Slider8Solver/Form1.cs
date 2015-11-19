using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Timers;
using System.Windows.Forms;

namespace Slider8Solver
{
    public partial class Form1 : Form
    {
        private SliderSolver solver;

        private System.Timers.Timer _toolTipTimer;

		public Form1()
		{
			InitializeComponent();

            UpdatePuzzleBoxSize();
        }


		#region Buttons
        private void buttonRandom_Click(object sender, EventArgs e)
        {
            int size;
            int.TryParse(comboBoxPuzzleSize.Text, out size);
            var puz = new Puzzle(size);
            textBoxPuzzleInput2.Text = string.Join(",", puz.StartingNode.State);
            UpdateTextBoxColors();
        }

        private void buttonSearchBreadthFirst_Click(object sender, EventArgs e)
        {
            StartSearch(labelBreadtFirstResult, SearchType.BreadthFirst);
        }

        private void buttonSearchDepthFirst_Click(object sender, EventArgs e)
        {
            StartSearch(labelDepthFirstResult, SearchType.DepthFirst);
        }

        private void buttonSearchAStar_Click(object sender, EventArgs e)
        {
	        richTextBoxOutput.Text = "";
            StartSearch(labelAStarResult, SearchType.AStar);
        }
        #endregion Buttons


        #region Events
        private void textBoxPuzzleInput_TextChanged(object sender, EventArgs e)
        {
            UpdatePuzzleWithContents();
        }

        private void textBoxPuzzleInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar.Equals(',') || char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxPuzzleSize.SelectedIndex = ((ComboBox) sender).SelectedIndex;

            UpdatePuzzleBoxSize();
        }
        #endregion Events


        #region ToolTip Controler
        private void TimedToolTip(int time, string text)
        {
            toolTip1.Show(text, this, PointToClient(MousePosition));
            _toolTipTimer = new System.Timers.Timer(1000);
            _toolTipTimer.Elapsed += ToolTipTimerElapsed;
            _toolTipTimer.Enabled = true;
            _toolTipTimer.Start();
        }

        private void ToolTipTimerElapsed(object source, ElapsedEventArgs e)
        {
            toolTip1.Active = false;
            toolTip1.Active = true;
        }
        #endregion ToolTip Controller


        private void StartSearch(Label lbl, SearchType type)
        {
            var puzzle = CreatePuzzleFromInput();

            if (puzzle == null) return;

            solver = new SliderSolver(puzzle);
            solver.SolvePuzzle(type);

            if (solver.Result == null) lbl.Text = "No Result";

            UpdateLabelWithPuzzleResults(solver, lbl);
			if (type == SearchType.AStar)
				OutputPuzzleSteps(solver);
        }
        
        private Puzzle CreatePuzzleFromInput()
        {
            int size;
            int.TryParse(comboBoxPuzzleSize.Text, out size);

            var tmpPuz = Puzzle.TryCreatePuzzle(textBoxPuzzleInput2.Text, size);
            if (tmpPuz != null) return tmpPuz;

            UpdateTextBoxColors(true);
            TimedToolTip(1500, "The current input is not valid.");
            return null;
        }

        private void UpdateLabelWithPuzzleResults(SliderSolver slvr, Label lbl)
        {
            if (slvr.Result == null)
            {
                lbl.Text = "Error!";
                return;
            }

            lbl.Text = "Starting: " + slvr.CurrentPuzzle.StartingNode.StateS
                + "\nSolution: " + slvr.Result.StateS
                + "\nDepth: " + $"{slvr.Result.Depth:n0}"
                + "\nNodes Expanded: " + $"{slvr.NodesExpanded:n0}"
                + "\nTime: " + $"{slvr.TimeSeconds:n4}";
        }

        private void OutputPuzzleSteps(SliderSolver slvr)
        {
            //foreach (var node in slvr.GetAnswerPath())
            //    richTextBoxOutput.AppendText(node + Environment.NewLine);
			
	        foreach (var move in slvr.GetAnswerMoves())
				richTextBoxOutput.AppendText(move + Environment.NewLine);

        }

        private void UpdateTextBoxColors(bool turnRed = false)
        {
            textBoxPuzzleInput2.BackColor = turnRed ? Color.Red : Color.White;
        }

        private void UpdatePuzzleBoxSize()
        {
            int size;
            int.TryParse(comboBoxPuzzleSize.Text, out size);
            var percent = 100/size;

            // Clear everything otu in prep for rebuilding
            tableLayoutPanelPuzzle.ColumnCount = 0;
            tableLayoutPanelPuzzle.RowCount = 0;
            tableLayoutPanelPuzzle.Controls.Clear();
            tableLayoutPanelPuzzle.ColumnStyles.Clear();
            tableLayoutPanelPuzzle.RowStyles.Clear();

            tableLayoutPanelPuzzle.ColumnCount = size;
            tableLayoutPanelPuzzle.RowCount = size;

            for (int col = 0; col < size; col++)
            {
                tableLayoutPanelPuzzle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percent));

                for (int row = 0; row < size; row++)
                {
                    if (col == 0)
                        tableLayoutPanelPuzzle.RowStyles.Add(new RowStyle(SizeType.Percent, percent));

                    var lbl = new Label
                    {
                        Text = "0",
                        Font = new Font("Microsoft Sans Serif", 20),
                        Dock = DockStyle.Fill,
                        Anchor = AnchorStyles.None,
                        AutoSize = true
                    };
                    tableLayoutPanelPuzzle.Controls.Add(lbl, col, row);
                }
            }

            UpdatePuzzleWithContents();
        }

        private void UpdatePuzzleWithContents()
        {
            int size;
            int.TryParse(comboBoxPuzzleSize.Text, out size);
            var contents = textBoxPuzzleInput2.Text.Split(',').ToList();
            

            int counter = 0;
            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++)
                {
                    var num = contents[counter];
                    if (num.Equals("0") || string.IsNullOrEmpty(num)) num = "";
                    tableLayoutPanelPuzzle.GetControlFromPosition(col,row).Text = num;
                    counter++;
                }
        }


    }
}
