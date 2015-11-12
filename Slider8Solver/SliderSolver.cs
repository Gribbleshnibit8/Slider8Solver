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
        public List<int> CurrentState;
        public string CurrentStateString;
        public string ParentState;
        public int Depth;
        public int Heuristic;

        public SliderNode(List<int> state, List<int> parent, int d)
        {
            CurrentState = state;
            CurrentStateString = StateToString(state);
            ParentState = StateToString(parent);
            Depth = d;
        }

        public SliderNode(List<int> state, List<int> parent, int d, int h)
        {
            CurrentState = state;
            CurrentStateString = StateToString(state);
            ParentState = StateToString(parent);
            Depth = d;
            Heuristic = h;
        }

        private static string StateToString(List<int> state)
        {
            return state.Aggregate("", (current, i) => current + i.ToString());
        }
    }

    public enum SearchType
    {
        DepthFirst,
        BreadthFirst,
        AStar
    }


	public class SliderSolver
	{

	    public Puzzle CurrentPuzzle;

		private readonly List<int> _currentState = new List<int>();
		private List<int> _goalState;

	    private Task<SliderNode> _solverTask;
	    private DateTime _startTime;
	    private DateTime _finishTime;


		private readonly Dictionary<string, SliderNode> _foundStates = new Dictionary<string, SliderNode>();

		private readonly Stack<List<int>> _stateStack = new Stack<List<int>>();

        private readonly Queue<List<int>> _stateQueue = new Queue<List<int>>();


        public SliderSolver(Puzzle newPuzzle)
        {
            CurrentPuzzle = newPuzzle;
            _currentState = CurrentPuzzle.StartingState;
        }


        #region Solvability
            /// <summary>
            /// Determines if a puzzle is solvable
            /// </summary>
            /// <returns>Returns true if puzzle can be solved</returns>
	        public static bool IsSolvable(Puzzle puzzle)
            {
                // Go ahead and get all this stuff cause we use most of it more than once
	            var isSizeOdd = IsOdd(puzzle.Size);
                var numInversionsEven = !IsOdd(GetInversionCount(puzzle));
                var blankOnOddRow = IsOdd(GetBlankRowFromBottom(puzzle));

                // Checking for solvability involves checking for inversion count and puzzle size
                // A puzzle is solvable if:
                // Puzzle size is odd and number of inversions is even
	            if ( (isSizeOdd && numInversionsEven) || (!isSizeOdd && (blankOnOddRow == numInversionsEven)) )
	                return true;
	            return false;
	        }

            /// <summary>
            /// Counts the numver of inversions in a given puzzle state.
            /// </summary>
            /// <returns>Returns the number of inversions in a puzzle</returns>
	        private static int GetInversionCount(Puzzle puzzle)
            {
                var state = puzzle.StartingState;
	            var inversionCount = 0;
	            var gridSize = puzzle.Size * puzzle.Size;

	            for (int i = 0; i < gridSize - 1; i++)
	                for (int j = i + 1; j < gridSize; j++)
	                    if ((state[j] != 0) && (state[i] != 0) && state[i] > state[j])
	                        inversionCount++;
	            return inversionCount;
	        }

            /// <summary>
            /// Determines if a number is odd or even
            /// </summary>
            /// <param name="num">Number to check</param>
            /// <returns>Returns true if number is odd, false if number is even.</returns>
            private static bool IsOdd(int num)
	        {
	            return num%2 != 0;
	        }

            /// <summary>
            /// Finds the row number that the blank space resides on
            /// </summary>
            /// <returns></returns>
            private static int GetBlankRowFromBottom(Puzzle puzzle)
	        {
	            var zeroIndex = puzzle.StartingState.IndexOf(0);

                // The index of the empty position divided by the puzzle size and ceiling'd
                // equals the row upon which the empty space resides.
                var row = (int)Math.Ceiling((decimal)zeroIndex / puzzle.Size);
                return puzzle.Size - row + 1;
	        }
        #endregion Solvability


        private SliderNode DepthFirstSolver()
	    {
            var depth = 0;
            var nodesExpanded = 0;
            SliderNode node;
            var stack = new Stack<SliderNode>();

            // Initialize the first node for the default state
            node = new SliderNode(_currentState, null, depth);

            // The first found state is the starting state
            _foundStates.Add(node.CurrentStateString, node);

            // Push the first state into the stack
            stack.Push(node);

            while(stack.Count > 0)
	        {
	            // get first in stack
	            var currentNode = stack.Pop();

                // Go ahead and increase the depth here, we'll be making new nodes shortly if necessary
                depth++;

                // Is state the goal state?
                if (currentNode.CurrentStateString.Equals(StateToString(_goalState)))
                {
                    // If yes return the state
                    return currentNode;
                    // Get path to root
                }

                // Otherwise get all swaps and push them to the queue
                // get swaps and perform them for next states, add to queue
                foreach (var swap in GetValidSwapIndices(currentNode.CurrentState))
                {
                    // Increment for each node expanded
                    nodesExpanded++;
                    var swapNode = new SliderNode(Swap(currentNode.CurrentState, swap), currentNode.CurrentState, depth);
                    if (_foundStates.ContainsKey(swapNode.CurrentStateString)) continue;
                    stack.Push(swapNode);
                    _foundStates.Add(swapNode.CurrentStateString, swapNode);
                }

            }


            return null;
        }

        private SliderNode BreadthFirstSolver()
        {
            var depth = 0;
            var nodesExpanded = 0;
            SliderNode node;
            var queue = new Queue<SliderNode>();


            // Initialize the first node for the default state
            node = new SliderNode(_currentState, null, depth);

            // The first found state is the starting state
            _foundStates.Add(node.CurrentStateString, node);

            // The first state on the queue is the starting state
            queue.Enqueue(node);


            // Start processing the queue
            while(queue.Count > 0)
            {
                // get first in queue
                var currentNode = queue.Dequeue();

                // Go ahead and increase the depth here, we'll be making new nodes shortly if necessary
                depth++;

                // Is state the goal state?
                if (currentNode.CurrentStateString.Equals(StateToString(_goalState)))
                {
                    // If yes return the state
                    return currentNode;
                    // Get path to root
                }

                // Otherwise get all swaps and push them to the queue
                // get swaps and perform them for next states, add to queue
                foreach (var swap in GetValidSwapIndices(currentNode.CurrentState))
                {
                    // Increment for each node expanded
                    nodesExpanded++;
                    var swapNode = new SliderNode(Swap(currentNode.CurrentState, swap), currentNode.CurrentState, depth);
                    if (_foundStates.ContainsKey(swapNode.CurrentStateString)) continue;
                    queue.Enqueue(swapNode);
                    _foundStates.Add(swapNode.CurrentStateString, swapNode);
                }
            }


            return null;
        }

        private SliderNode AStarSolver()
	    {

            return null;
        }


        #region PuzzleSolver
            public void SolvePuzzle(SearchType type)
		    {
                CreateGoalState();

                switch (type)
                {
                    case SearchType.DepthFirst:
                        _solverTask = new Task<SliderNode>(DepthFirstSolver);
                        break;
                    case SearchType.BreadthFirst:
                        _solverTask = new Task<SliderNode>(BreadthFirstSolver);
                        break;
                    case SearchType.AStar:
                        _solverTask = new Task<SliderNode>(AStarSolver);
                        break;
                }

                _solverTask.Start();
			    _solverTask.ContinueWith(SolverComplete);
                _startTime = DateTime.Now;
		    }

		    private void SolverComplete(Task t)
		    {
		        if (!t.IsCompleted) return;

		        // Do completed action
		        _finishTime = DateTime.Now;
		        Console.WriteLine("Solver Completed");
		    }

		    private void CreateGoalState()
		    {
			    _goalState = new List<int>();
			    _goalState.AddRange(Enumerable.Range(1, CurrentPuzzle.Size));
			    _goalState.Add(0);
		    }
		#endregion PuzzleSolver


		private static string StateToString(List<int> state)
		{
			return state.Aggregate("", (current, i) => current + i.ToString());
		}


		private IEnumerable<int> GetValidSwapIndices(IList<int> state)
		{
			var emptyIndex = state.IndexOf(0);
			var validIndices = new List<int>();

            // If modulus of index is 0 or CurrentPuzzle.Size-1 then it is an edge
            // If 0 it is leading edge, can only swap with index + 1
            // If CurrentPuzzle.Size-1  it is a trailing edge, can only swap with index - 1

            // Edge check
            var position = emptyIndex % CurrentPuzzle.Size;

			// Leading edge
			if (position.Equals(0))
				validIndices.Add(emptyIndex + 1);

			// Trailing edge
			else if (position.Equals(CurrentPuzzle.Size - 1))
				validIndices.Add(emptyIndex - 1);

			// Middle position
			else
			{
				validIndices.Add(emptyIndex + 1);
				validIndices.Add(emptyIndex - 1);
			}

			// Add indices above and below the empty space, if valid
			if (!(emptyIndex - CurrentPuzzle.Size <= 0)) validIndices.Add(emptyIndex - CurrentPuzzle.Size);
			if (_currentState.Count >= emptyIndex + CurrentPuzzle.Size) validIndices.Add(emptyIndex + CurrentPuzzle.Size);

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
