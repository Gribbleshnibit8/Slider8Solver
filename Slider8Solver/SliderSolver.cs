using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Slider8Solver
{

    public enum SearchType
    {
        DepthFirst,
        BreadthFirst,
        AStar
    }

	/// <summary>
	/// 
	/// </summary>
    public class SliderSolver
    {
		#region Public Members
		public Puzzle CurrentPuzzle { get; }

		/// <summary>
		/// Gets the result node of a completed search.
		/// </summary>
        public SliderNode Result { get; private set; }
        
		/// <summary>
		/// Gets the time required to solve the puzzle.
		/// </summary>
        public double TimeSeconds => (_finishTime - _startTime).TotalMilliseconds/1000;
        
		/// <summary>
		/// Total count of the nodes expanded by the last used search algorithm.
		/// </summary>
        public int NodesExpanded;

		/// <summary>
		/// The last search algorithm used to solve this puzzle.
		/// </summary>
	    public SearchType LastSearchType { get; private set; }
		#endregion Public Members


		#region Private Members
		/// <summary>
		/// Tracks the start time of the solver.
		/// </summary>
		private DateTime _startTime;

		/// <summary>
		/// Tracks the finish time of the solver.
		/// </summary>
        private DateTime _finishTime;
        
		/// <summary>
		/// Stores all visited states on the current search path.
		/// </summary>
        private readonly SortedList<string, SliderNode> _history = new SortedList<string, SliderNode>();
		#endregion Private Members


		#region Ctors
		/// <summary>
		/// Initializes a new instance of the SliderSolver class that takes an instance of a puzzle to solve.
		/// </summary>
		/// <param name="puz">The puzzle instance that this solver will be used to solve.</param>
		public SliderSolver(Puzzle puz)
		{
			CurrentPuzzle = puz;
		}
		#endregion Ctors


		#region Search Types
		/// <summary>
		/// Runs a Breadth First search through the puzzle, looking for the solution.
		/// </summary>
		/// <returns>Node holding the solved puzzle, or nothing if puzzle not solved.</returns>
		private SliderNode BreadthFirstSolver()
        {
            // Initialize the first node for the default state, add it to found state list,
            // then push it to the queue
            var node = CurrentPuzzle.StartingNode;
            var queue = new Queue<SliderNode>();

            _history.Add(node.StateS, node);

            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                // get first in queue
                var currentNode = queue.Dequeue();

                // Increment for each node expanded
                NodesExpanded++;

                // Is state the goal state?
                if (currentNode.StateS.Equals(CurrentPuzzle.GoalNode.StateS)) return currentNode;

                // Get all swaps and push them to the queue
                foreach (var swapNode in currentNode.GetSuccessors())
                {
                    // If the swapped state is already processed, continue
                    if (_history.ContainsKey(swapNode.StateS)) continue;
                    _history.Add(swapNode.StateS, swapNode);

                    queue.Enqueue(swapNode);
                }
            }

            return null;
        }

		/// <summary>
		/// Runs a Depth First search through the puzzle, looking for the solution.
		/// </summary>
		/// <returns>Node holding the solved puzzle, or nothing if puzzle not solved.</returns>
		private SliderNode DepthFirstSolver()
        {
            // Initialize the first node for the default state, add it to found state list,
            // then push it to the stack
            var node = CurrentPuzzle.StartingNode;
            var stack = new Stack<SliderNode>();
            
            _history.Add(node.StateS, node);

            stack.Push(node);

            while (stack.Count > 0)
            {
                // get first in stack
                var currentNode = stack.Pop();

                // Increment for each node expanded
                NodesExpanded++;

                // Is state the goal state?
                if (currentNode.StateS.Equals(CurrentPuzzle.GoalNode.StateS)) return currentNode;

                // Get all swaps and push them to the queue
                foreach (var swapNode in currentNode.GetSuccessors())
                {
                    if (_history.ContainsKey(swapNode.StateS)) continue;
                    _history.Add(swapNode.StateS, swapNode);

                    stack.Push(swapNode);
                }
            }

            return null;
        }

		/// <summary>
		/// Runs an AStar search through the puzzle, looking for the solution.
		/// </summary>
		/// <returns>Node holding the solved puzzle, or nothing if puzzle not solved.</returns>
		private SliderNode AStarSolver()
        {
            // Initialize the first node for the default state, add it to found state list,
            // then push it to the stack
            var node = CurrentPuzzle.StartingNode;

            var comp = new ClassComparer();
            var heuristicList = new SortedList<int, SliderNode>(comp);
            
            _history.Add(node.StateS, node);
            heuristicList.Add(node.Heuristic + node.Cost, node);

            while (heuristicList.Count > 0)
            {
                // get first in stack
                var currentNode = heuristicList.First().Value;
                heuristicList.RemoveAt(0);

                // Increment for each node expanded
                NodesExpanded++;

                // Is state the goal state?
                if (currentNode.StateS.Equals(CurrentPuzzle.GoalNode.StateS)) return currentNode;

                // Get all swaps and push them to the queue
                foreach (var swapNode in currentNode.GetSuccessors())
                {
                    var key = swapNode.StateS;

                    // If key is in history and the cost in the history is larger, then reduce the cost in the history
                    // 
                    if (_history.ContainsKey(key))
                    {
                        if (_history[key].Cost <= swapNode.Cost) continue;
                        _history[key].Cost = swapNode.Cost;
                        heuristicList.Add(swapNode.Heuristic + swapNode.Cost, swapNode);
                    }
                    else
                    {
                        heuristicList.Add(swapNode.Heuristic + swapNode.Cost, swapNode);
                        _history.Add(swapNode.StateS, swapNode);
                    }
                }
            }
            return null;
        }
		#endregion Search Types

		
		/// <summary>
		/// Runs a SearchType search with the solver.
		/// </summary>
		/// <param name="searchType">The SearchType search to perform with the solver.</param>
		public void SolvePuzzle(SearchType searchType)
        {
            _startTime = DateTime.Now;
	        LastSearchType = searchType;

            switch (searchType)
            {
                case SearchType.DepthFirst:
                    Result = DepthFirstSolver();
                    break;

                case SearchType.BreadthFirst:
                    Result = BreadthFirstSolver();
                    break;

                case SearchType.AStar:
                    Result = AStarSolver();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(searchType), searchType, null);
            }
            _finishTime = DateTime.Now;
        }

		/// <summary>
		/// Creates a list of nodes leading from the starting state to the result state.
		/// </summary>
		/// <returns>Returns a List&lt;SliderNode&gt; of nodes in order from starting state to result state.</returns>
		public List<SliderNode> GetAnswerPath()
		{
			var answerList = new List<SliderNode>();

			var currentNode = Result;
			answerList.Add(Result);

			// So long as the nodes have parents, add them to the list.
			// This creates a reverse order list, so we have to reverse 
			// it to return it in the correct order.
			while (!string.IsNullOrEmpty(currentNode.ParentS))
			{
				currentNode = _history[currentNode.ParentS];
				answerList.Add(currentNode);
			}
			answerList.Reverse();
			return answerList;
		}

		/// <summary>
		/// Creates a list of plaintext move directions for solving a puzzle.
		/// </summary>
		/// <returns>Returns a List&lt;string&gt; of directions for moves to solve the puzzle.</returns>
		public List<string> GetAnswerMoves()
		{
			var moves = new List<string>();
			var answerPath = GetAnswerPath();

			// To find the direction that a tile was moved we need to look at where the empty square gets moved to.
			// Then look at the position it was and get the number there to return the tile moved.
			for (var index = 0; index < answerPath.Count - 1; index++)
			{
				var posEmpty = answerPath[index].GetCellPositionOfValue(0);
				var posEmptySuc = answerPath[index + 1].GetCellPositionOfValue(0);

				if (posEmptySuc.X < posEmpty.X)
					moves.Add("Move " + answerPath[index].GetValueOfCellPosition(posEmptySuc) + " right");
				else if (posEmptySuc.X > posEmpty.X)
					moves.Add("Move " + answerPath[index].GetValueOfCellPosition(posEmptySuc) + " left");
				else if (posEmptySuc.Y < posEmpty.Y)
					moves.Add("Move " + answerPath[index].GetValueOfCellPosition(posEmptySuc) + " down");
				else if (posEmptySuc.Y > posEmpty.Y)
					moves.Add("Move " + answerPath[index].GetValueOfCellPosition(posEmptySuc) + " up");

			}


			return moves;
		}
	}
}