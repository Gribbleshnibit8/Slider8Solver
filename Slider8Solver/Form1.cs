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
			// For all values that are not comma, number, or controls, mark the event
			// as handled to prevent the data from being entered.
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


		/// <summary>
		/// Starts a given search type.
		/// </summary>
		/// <param name="lbl">The label output will be written to.</param>
		/// <param name="type">The search type to perform.</param>
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
        
		/// <summary>
		/// Creates a new puzzle from the given input and size. If the input is not valid, returns nothing and marks the entry box red to notify error.
		/// </summary>
		/// <returns></returns>
        private Puzzle CreatePuzzleFromInput()
        {
            int size;
            int.TryParse(comboBoxPuzzleSize.Text, out size);

            var tmpPuz = Puzzle.TryCreatePuzzle(textBoxPuzzleInput2.Text, size);
            if (tmpPuz != null) return tmpPuz;

			// Handle an invalid puzzle by marking the text box in red and showing a tooltip to let the user know.
            UpdateTextBoxColors(true);
            TimedToolTip(1500, "The current input is not valid.");
            return null;
        }

		/// <summary>
		/// Updates a given label control text with either an error message or the results from a given solver.
		/// </summary>
		/// <param name="slvr"></param>
		/// <param name="lbl"></param>
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

		/// <summary>
		/// Retrieves a step-by-step solution from the solver for solving a puzzle.
		/// </summary>
		/// <param name="slvr"></param>
        private void OutputPuzzleSteps(SliderSolver slvr)
        {
	        foreach (var move in slvr.GetAnswerMoves())
				richTextBoxOutput.AppendText(move + Environment.NewLine);
        }

		/// <summary>
		/// Swaps text box background colors.
		/// </summary>
		/// <param name="turnRed"></param>
        private void UpdateTextBoxColors(bool turnRed = false)
        {
            textBoxPuzzleInput2.BackColor = turnRed ? Color.Red : Color.White;
        }

		/// <summary>
		/// Redraws the TableLayoutPanel control for a new puzzle size. SLOW!
		/// </summary>
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

            for (var col = 0; col < size; col++)
            {
                tableLayoutPanelPuzzle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percent));

                for (var row = 0; row < size; row++)
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

		/// <summary>
		/// Converts the contents of the text box into the contents of the TableLayoutPanel
		/// </summary>
        private void UpdatePuzzleWithContents()
        {
            int size;
            int.TryParse(comboBoxPuzzleSize.Text, out size);
            var contents = textBoxPuzzleInput2.Text.Split(',').ToList();
            
			// TableLayoutPanel is weird, it holds everything in an IEnumberable as a single list,
			// but each control in that list has an x,y value that determines its row and column.
			// It's annoying to work with but this is the best way I could figure to do it.
            int counter = 0;
            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++)
                {
					if (counter >= contents.Count) return;
                    var num = contents[counter];
                    if (num.Equals("0") || string.IsNullOrEmpty(num)) num = "";
                    tableLayoutPanelPuzzle.GetControlFromPosition(col,row).Text = num;
                    counter++;
                }
        }


    }
}
