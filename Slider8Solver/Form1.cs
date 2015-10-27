using System;
using System.Drawing;
using System.Windows.Forms;

namespace Slider8Solver
{
    public partial class Form1 : Form
	{

		private SliderSolver solver;

		public Form1()
		{
			InitializeComponent();

			solver = new SliderSolver();
		}

		#region Buttons

		private void buttonUseInput_Click(object sender, EventArgs e)
		{
			int size;
			Int32.TryParse(comboBox1.Text, out size);
			solver = new SliderSolver(textBoxPuzzleInput.Text, size);
		    if (!solver.IsPuzzleValid)
		        textBoxPuzzleInput.BackColor = Color.Red;
		    else
		        textBoxPuzzleInput.BackColor = Color.White;

            solver.SolvePuzzle();
		}

        private void buttonReset_Click(object sender, EventArgs e)
        {
            var isSolvable = solver.IsPuzzleValid;

            Console.WriteLine("Puzzle is solvable: " + isSolvable);
        }

		#endregion Buttons


		




	}
}
