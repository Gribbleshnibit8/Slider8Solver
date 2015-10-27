using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slider8Solver
{
    public class SliderNode
    {
        public List<int> currentState;
        public string currentStateString;
        public int depth;
        public int heuristic;

        public SliderNode(List<int> state, string stateString, int d, int h)
        {
            currentState = state;
            currentStateString = stateString;
            depth = d;
            heuristic = h;
        }
    }



	public class SliderSolver
	{

		private readonly List<int> _elementList = new List<int>();
		private List<int> _goalState;

        /// <summary>
        /// The width/height value of the puzzle, given as the length of one side of the puzzle space.
        /// </summary>
		private readonly int _sliderSize = 0;


	    private Task _solverTask;
	    private DateTime _startTime;


		private readonly Dictionary<string, SliderNode> _foundStates = new Dictionary<string, SliderNode>();

		private readonly Stack<List<int>> _stateStack = new Stack<List<int>>();



        /// <summary>
        /// Set to true if the input size is equal to the puzzle size.
        /// </summary>
	    private readonly bool _inputValidated = false;

        /// <summary>
        /// Is true if the puzzle is a valid, solvable puzzle.
        /// </summary>
	    public bool IsPuzzleValid = false;

		#region Constructors
		    public SliderSolver()
		    {
			    // Generate a random puzzle to solve
		    }

		    public SliderSolver(string input, int size)
		    {
			    _sliderSize = size;

			    var nums = input.Split(',').ToList();

                if (nums.Count != size*size)
                    return;

		        _inputValidated = true;

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

		        IsPuzzleValid = IsSolvable();
		    }

		    public SliderSolver(List<int> input, int size)
		    {
                if (input.Count != size*size)
                    return;

                _inputValidated = true;

			    _sliderSize = size;
			    _elementList = input;

		        IsPuzzleValid = IsSolvable();
		    }
        #endregion Constructors


        #region Solvability
            /// <summary>
            /// Determines if a puzzle is solvable
            /// </summary>
            /// <returns>Returns true if puzzle can be solved</returns>
	        private bool IsSolvable()
            {
                if (!_inputValidated)
                    return false;

                // Go ahead and get all this stuff cause we use most of it more than once
	            var isSizeOdd = IsOdd(_sliderSize);
                var numInversionsEven = !IsOdd(GetInversionCount());
                var blankOnOddRow = IsOdd(GetBlankRowFromBottom());

                // Checking for solvability involves checking for inversion count and puzzle size
                // A puzzle is solvable if:
                // Puzzle size is odd and number of inversions is even
                // 
	            if ( (isSizeOdd && numInversionsEven) || (!isSizeOdd && (blankOnOddRow == numInversionsEven)) )
	                return true;
	            return false;
	        }

            /// <summary>
            /// Counts the numver of inversions in a given puzzle state.
            /// </summary>
            /// <returns>Returns the number of inversions in a puzzle</returns>
	        private int GetInversionCount()
	        {
	            var inversionCount = 0;
	            var gridSize = _sliderSize*_sliderSize;

	            for (int i = 0; i < gridSize - 1; i++)
	                for (int j = i + 1; j < gridSize; j++)
	                    if ((_elementList[j] != 0) && (_elementList[i] != 0) && _elementList[i] > _elementList[j])
	                        inversionCount++;
	            return inversionCount;
	        }

            /// <summary>
            /// Determines if a number is odd or even
            /// </summary>
            /// <param name="num">Number to check</param>
            /// <returns>Returns true if number is odd, false if number is even.</returns>
	        private bool IsOdd(int num)
	        {
	            return num%2 != 0;
	        }

            /// <summary>
            /// Finds the row number that the blank space resides on
            /// </summary>
            /// <returns></returns>
	        private int GetBlankRowFromBottom()
	        {
	            var zeroIndex = _elementList.IndexOf(0);

                // The index of the empty position divided by the puzzle size and ceiling'd
                // equals the row upon which the empty space resides.
                var row = (int)Math.Ceiling((decimal)zeroIndex / _sliderSize);
                return _sliderSize - row + 1;
	        }
        #endregion Solvability


        #region Depth First Search
            
        #endregion Depth First Search


        #region PuzzleSolver
        public void SolvePuzzle()
		    {
			    _solverTask = new Task(Solver);
                
			    _solverTask.Start();
			    _solverTask.ContinueWith(SolverComplete);
                _startTime = DateTime.Now;
		    }

		    private void Solver()
		    {
			    CreateGoalState();
                
			    // Add starting state to dictionary
			    _foundStates.Add(StateToString(_elementList), new SliderNode(_elementList, StateToString(_elementList), 0, 0));
                
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
			        var finishTime = DateTime.Now;
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
