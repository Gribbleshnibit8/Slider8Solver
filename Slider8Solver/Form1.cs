using System;
using System.Drawing;
using System.Security.Cryptography;
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
		    tabControl1.SelectedTab = tabPage2;
		}

		#region Buttons

		private void buttonUseInput_Click(object sender, EventArgs e)
		{
            var puzzle = CreatePuzzleFromInput();

            if (puzzle == null) return;

            
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonSearchBreadthFirst_Click(object sender, EventArgs e)
        {
            var puzzle = CreatePuzzleFromInput();

            if (puzzle == null) return;

            solver = new SliderSolver(puzzle);
            solver.SolvePuzzle(SearchType.BreadthFirst);
        }

        private void buttonSearchDepthFirst_Click(object sender, EventArgs e)
        {

        }

        private void buttonSearchAStar_Click(object sender, EventArgs e)
        {

        }

        #endregion Buttons

        #region Events
        private void textBoxPuzzleInput_TextChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Equals(tabPage1))
                textBoxPuzzleInput2.Text = ((TextBox)sender).Text;
            else if (tabControl1.SelectedTab.Equals(tabPage2))
                textBoxPuzzleInput.Text = ((TextBox)sender).Text;
        }
        #endregion Events


        #region Get Input

        private Puzzle CreatePuzzleFromInput()
        {
            int size;
            int.TryParse(comboBox1.Text, out size);

            var defaultPuzzle = new Puzzle();
            if (defaultPuzzle.NewPuzzle(textBoxPuzzleInput.Text, size))
            {
                textBoxPuzzleInput.BackColor = Color.White;
                textBoxPuzzleInput2.BackColor = Color.White;
                return defaultPuzzle;
            }

            textBoxPuzzleInput.BackColor = Color.Red;
            textBoxPuzzleInput2.BackColor = Color.Red;
            TimedToolTip(1500, "The current input is not valid.");
            return null;
        }
        #endregion Get Input



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
        }
        #endregion ToolTip Controller

        
    }
}
