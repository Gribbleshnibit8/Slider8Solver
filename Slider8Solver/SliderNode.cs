using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Slider8Solver
{
    public class SliderNode
    {
		#region Public Members
		/// <summary>
		/// Active puzzle state.
		/// </summary>
		public List<int> State;

		/// <summary>
		/// Active puzzle state string.
		/// </summary>
        public string StateS => StateToString(State);

		/// <summary>
		/// Parent state string.
		/// </summary>
        public string ParentS { get; private set; }

		/// <summary>
		/// Depth value of this node in the search tree.
		/// </summary>
        public int Depth { get; private set; }

		/// <summary>
		/// Numver of misplaced tiles in the node.
		/// </summary>
	    public int Heuristic => GetMisplacedTiles();

		/// <summary>
		/// Cost to get to this node from the starting point.
		/// </summary>
        public int Cost;
		#endregion Public Members

		/// <summary>
		/// Gets the size of a side of the puzzle state.
		/// </summary>
		private int Size => Math.Abs((int) Math.Sqrt(State.Count));


        #region Ctors
		/// <summary>
		/// Initializes a new instance of the SliderNode class with the specified state.
		/// </summary>
		/// <param name="state"></param>
        public SliderNode(List<int> state)
        {
            State = state;
        }

		/// <summary>
		/// Initializes a new instance of the SliderNode class with the specified state and depth.
		/// </summary>
		/// <param name="state"></param>
		/// <param name="d"></param>
        public SliderNode(List<int> state, int d) : this(state)
        {
            Depth = d;
        }
        #endregion Ctors


        #region Successors
		/// <summary>
		/// Creates a list of nodes that are successors of the current node.
		/// </summary>
		/// <returns>List&lt;SliderNode&gt; of successor nodes.</returns>
        public List<SliderNode> GetSuccessors()
        {
            var successors = new List<SliderNode>();
            foreach (var index in GetValidSwapIndices())
            {
				successors.Add(new SliderNode(Swap(index))
								{
									Depth = Depth + 1,
									Cost = Cost + 1,
									ParentS = StateS
								}
				);
            }
            return successors;
        }

		/// <summary>
		/// Gets a list of valid indices that the empty node can be moved into.
		/// </summary>
		/// <returns>A List&lt;int&gt; of indicies.</returns>
        private List<int> GetValidSwapIndices()
        {
            var emptyIndex = State.IndexOf(0);
            var validIndices = new List<int>();

            // If modulus of index is 0 or CurrentPuzzle.Size-1 then it is an edge
            // If 0 it is leading edge, can only swap with index + 1
            // If CurrentPuzzle.Size-1  it is a trailing edge, can only swap with index - 1

            // Edge check
            var position = emptyIndex % Size;

            // Leading edge
            if (position.Equals(0))
                validIndices.Add(emptyIndex + 1);

            // Trailing edge
            else if (position.Equals(Size - 1))
                validIndices.Add(emptyIndex - 1);

            // Middle position
            else
            {
                validIndices.Add(emptyIndex + 1);
                validIndices.Add(emptyIndex - 1);
            }

            // Add indices above and below the empty space, if valid
            if (!(emptyIndex - Size <= 0)) validIndices.Add(emptyIndex - Size);
            if ((Size*Size) > emptyIndex + Size) validIndices.Add(emptyIndex + Size);

            // Sort out the values so they are always in the same order, search algos are invalid otherwise
            validIndices.Sort();
            return validIndices;
        }

		/// <summary>
		/// Performs a swap between the empty node and the given index.
		/// </summary>
		/// <param name="index">The index of the number to swap with the empty node.</param>
		/// <returns>Returns a new list containing the swapped nodes.</returns>
        private List<int> Swap(int index)
        {
            var state = new List<int>(State);
            var temp = state[index];
            state[state.IndexOf(0)] = temp;
            state[index] = 0;
            return state;
        }
		#endregion Successors
		

		/// <summary>
		/// Gets the number of misplaced tiles in a given List&lt;int&gt;
		/// </summary>
		/// <returns>Count of misplaced tiles.</returns>
		public int GetMisplacedTiles()
		{
			var misplacedTiles = 0;
			//var goal = CurrentPuzzle.GoalNode.State;

			// Count each tile that is out of place, skipping if the misplaced tile
			// is the blank tile, as it is never out of place.
			for (var j = 0; j < (Size * Size); j++)
				if (!State[j].Equals(j+1) && !State[j].Equals(0))
					misplacedTiles++;

			return misplacedTiles;
		}

		/// <summary>
		/// Gets the 2D coordinates of a given value in the current state.
		/// </summary>
		/// <param name="value">The value to find in the current state</param>
		/// <returns>A new Point coordinate containing the X/Y position of the value in the state.</returns>
		public Point GetCellPositionOfValue(int value)
	    {
			return new Point(State.IndexOf(value)%Size, State.IndexOf(value)/Size);
	    }

		/// <summary>
		/// Converts a Point coordinate into the index position in the state.
		/// </summary>
		/// <param name="position">The point X/Y coordinate to convert from.</param>
		/// <returns>The value at the given Point.</returns>
	    public int GetValueOfCellPosition(Point position)
	    {
		    var index = position.X + Size*position.Y;
            return State[index];
	    }

		/// <summary>
		/// Converts a state List&lt;int&gt; to a string.
		/// </summary>
		/// <param name="state">The state to convert</param>
		/// <returns>The state as a string</returns>
        public static string StateToString(List<int> state)
        {
            if (state == null) return null;

            return state.Aggregate("", (current, i) => current + i.ToString());
        }

		/// <summary>
		/// Outputs the current node state as a string in a Size x Size multiple line output.
		/// </summary>
		/// <returns></returns>
        public override string ToString()
        {
            var index = 0;
            var s = new StringWriter();
            for (int row = 1; row <= Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    s.Write(State[index]);
                    index++;
                }
                s.WriteLine();
            }
            return s.ToString();
        }
    }
}
