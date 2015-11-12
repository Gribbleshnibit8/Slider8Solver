using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slider8Solver
{
    public class Puzzle
    {
        public bool IsSolvable => SliderSolver.IsSolvable(this);

        public int Size { get; set; }

        private List<int> _startingState = new List<int>();
        public List<int> StartingState
        {
            get { return _startingState; }
            set
            {
                if (_startingState.Count > 0)
                    _startingState = value;
            }
        }

        public List<int> CurrentState { get; set; }


        public void CreateRandomPuzzle()
        {
            if (_startingState == null)
                return;
                // create random puzzle
        }

        /// <summary>
        /// Creates a new puzzle.
        /// </summary>
        /// <param name="input">Comma separate string of numbers to convert to a puzzle</param>
        /// <param name="size">The size of the puzzle, number of squares on one side.</param>
        /// <returns>Returns false if the puzzle's input does not match the size of the puzzle.</returns>
        public bool NewPuzzle(string input, int size)
        {
            var nums = input.Split(',').ToList();

            if (nums.Count != size * size)
                return false;

            Size = size;

            foreach (var s in nums)
            {
                int num;
                if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s) || s.Equals("0"))
                    _startingState.Add(0);
                else if (int.TryParse(s, out num))
                    _startingState.Add(num);
                else
                {
                    MessageBox.Show("Invalid number entered. '" + s + "' is not a number", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }

            CurrentState = _startingState;

            return true;
        }

        public bool NewPuzzle(List<int> input, int size)
        {
            if (input.Count != size * size)
                return false;

            Size = size;
            _startingState = input;
            CurrentState = _startingState;

            return true;
        }

    }
}
