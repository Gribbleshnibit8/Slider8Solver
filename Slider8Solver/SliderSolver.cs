using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slider8Solver
{
	public class SliderSolver
	{

		private readonly List<int> _elementList = new List<int>();
		private List<int> _goalState;
		private readonly int _sliderSize = 0;

		private readonly Dictionary<string, int> _foundStates = new Dictionary<string, int>();

		private readonly Stack<List<int>> _stateStack = new Stack<List<int>>();

		#region Constructors
		public SliderSolver()
		{
			// Generate a random puzzle to solve
		}

		public SliderSolver(string input, int size)
		{
			_sliderSize = size;

			var nums = input.Split(',').ToList();

			foreach (var s in nums)
			{
				int num;
				if (String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s) || s.Equals("0"))
					_elementList.Add(0);
				else if (int.TryParse(s, out num))
					_elementList.Add(num);
				else
				{
					MessageBox.Show("Invalid number entered. '" + s + "' is not a number", "Error", MessageBoxButtons.OK,
						MessageBoxIcon.Error);
				}
			}
		}

		public SliderSolver(List<int> input, int size)
		{
			_sliderSize = size;
			_elementList = input;

		}
		#endregion Constructors


		#region PuzzleSolver
		public void SolvePuzzle()
		{
			var solverTask = new Task(Solver);

			solverTask.Start();
			solverTask.ContinueWith(SolverComplete);

		}

		private void Solver()
		{
			CreateGoalState();

			// Add starting state to dictionary
			_foundStates.Add(StateToString(_elementList), 0);

			_stateStack.Push(_elementList);

			var currentState = _elementList;

			while (StateToString(currentState) != StateToString(_goalState))
			{
				currentState = _stateStack.Pop();

				var swapIndices = GetValidSwapIndices(currentState);

				foreach (var swapIndex in swapIndices)
				{
					var swapped = Swap(currentState, swapIndex);
					if (!_foundStates.ContainsKey(StateToString(swapped)))
					{
						_foundStates.Add(StateToString(swapped), 0);
						_stateStack.Push(swapped);
					}
				}


			}
		}

		private static void SolverComplete(Task t)
		{
			if (t.IsCompleted)
			{
				// Do completed action
				Console.WriteLine("Solver Completed");
			}
		}

		private void CreateGoalState()
		{
			_goalState = new List<int>();
			_goalState.AddRange(Enumerable.Range(1, _sliderSize));
			_goalState.Add(0);
		}
		#endregion PuzzleSolver


		private static String StateToString(List<int> state)
		{
			return state.Aggregate("", (current, i) => current + i.ToString());
		}


		private IEnumerable<int> GetValidSwapIndices(List<int> state)
		{
			var emptyIndex = state.IndexOf(0);
			var validIndices = new List<int>();

			// If modulus of index is 0 or _sliderSize-1 then it is an edge
			// If 0 it is leading edge, can only swap with index + 1
			// If _sliderSize-1  it is a trailing edge, can only swap with index - 1

			// Edge check
			var position = emptyIndex % _sliderSize;

			// Leading edge
			if (position.Equals(0))
				validIndices.Add(emptyIndex + 1);

			// Trailing edge
			else if (position.Equals(_sliderSize - 1))
				validIndices.Add(emptyIndex - 1);

			// Middle position
			else
			{
				validIndices.Add(emptyIndex + 1);
				validIndices.Add(emptyIndex - 1);
			}

			// Add indices above and below the empty space, if valid
			if (!(emptyIndex - _sliderSize <= 0)) validIndices.Add(emptyIndex - _sliderSize);
			if (_elementList.Count >= emptyIndex + _sliderSize) validIndices.Add(emptyIndex + _sliderSize);

			//783415602;

			validIndices.Sort();
			return validIndices;
		}


		private static List<int> Swap(List<int> state, int index)
		{
			var temp = state[index];
			state[state.IndexOf(0)] = temp;
			state[index] = 0;
			return state;
		}
	}
}
