using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		private void button3_Click(object sender, EventArgs e)
		{
			int size;
			Int32.TryParse(comboBox1.Text, out size);
			solver = new SliderSolver(textBox1.Text, size);
		}

		#endregion Buttons


		




	}
}
