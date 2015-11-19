using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Slider8Solver
{
    public class Puzzle
    {
        public int Size { get; set; }
        public int SquareSize => Size*Size;

        public readonly SliderNode GoalNode;
        public readonly SliderNode StartingNode;


        /// <summary>
        /// Creates a random puzzle
        /// </summary>
        public Puzzle(int size)
        {
            Size = size;
            GoalNode = CreateGoalState(size);

            var array = GoalNode.State.ToArray();
            do
            {
                Shuffle(array);
                StartingNode = new SliderNode(array.ToList());
            } while (!IsSolvable());
        }

        /// <summary>
        /// Creates a new puzzle with the given input and size
        /// </summary>
        /// <param name="input"></param>
        /// <param name="size"></param>
        public Puzzle(string input, int size)
        {
            Size = size;
            GoalNode = CreateGoalState(size);

            var list = new List<int>();
            foreach (var s in input.Split(',').ToList())
            {
                int num;
                if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s) || s.Equals("0"))
                    list.Add(0);
                else if (int.TryParse(s, out num))
                    list.Add(num);
            }

            StartingNode = new SliderNode(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Puzzle TryCreatePuzzle(string input, int size)
        {
            var nums = input.Split(',').ToList();

            // Do error checking for invalid states.
            if (nums.Count != size*size) return null;                       // number of items and size don't match
            if (nums.GroupBy(n => n).Any(g => g.Count() > 1)) return null;  // duplicate numbers in list

            return (nums.Count != size * size) ? null : new Puzzle(input, size);
        }


        #region Solvability
        /// <summary>
        /// Determines if a puzzle is solvable
        /// </summary>
        /// <returns>Returns true if puzzle can be solved</returns>
        public bool IsSolvable()
        {
            // Go ahead and get all this stuff cause we use most of it more than once
            var isSizeOdd = IsOdd(Size);
            var numInversionsEven = !IsOdd(GetInversionCount());
            var blankOnOddRow = IsOdd(GetBlankRowFromBottom());

            // Checking for solvability involves checking for inversion count and puzzle size
            // A puzzle is solvable if:
            // Puzzle size is odd and number of inversions is even
            return (isSizeOdd && numInversionsEven) || (!isSizeOdd && (blankOnOddRow == numInversionsEven));
        }

        /// <summary>
        /// Counts the numver of inversions in a given puzzle state.
        /// </summary>
        /// <returns>Returns the number of inversions in a puzzle</returns>
        private int GetInversionCount()
        {
            var state = StartingNode.State;
            var inversionCount = 0;

			// Inversions are when a tile preceeds a tile with a lower number. This counts for
			// all tiles after the high value tile, so each tile must be checked against all
			// remaining tiles in the state.
            for (int i = 0; i < SquareSize - 1; i++)
                for (int j = i + 1; j < SquareSize; j++)
                    if ((state[j] != 0) && (state[i] != 0) && state[i] > state[j])
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
            return num % 2 != 0;
        }

        /// <summary>
        /// Finds the row number that the blank space resides on
        /// </summary>
        /// <returns></returns>
        private int GetBlankRowFromBottom()
        {
            var zeroIndex = StartingNode.State.IndexOf(0);

            // The index of the empty position divided by the puzzle size and ceiling'd
            // equals the row upon which the empty space resides.
            var row = (int)Math.Ceiling((decimal)zeroIndex / Size);
            return Size - row + 1;
        }
        #endregion Solvability

		/// <summary>
		/// Generates a goal state for a given size puzzle.
		/// </summary>
		/// <param name="size">The size in one side of the puzzle for which to generate a goal state.</param>
		/// <returns>SliderNode containing a goal state.</returns>
        public static SliderNode CreateGoalState(int size)
        {
            var goal = new List<int>();
            goal.AddRange(Enumerable.Range(1, size * size - 1));
            goal.Add(0);
            return new SliderNode(goal);
        }

        /// <summary>
        /// Shuffle the array.
        /// </summary>
        /// <typeparam name="T">Array element type.</typeparam>
        /// <param name="array">Array to shuffle.</param>
        static void Shuffle<T>(T[] array)
        {
            var n = array.Length;
            var random = new Random();
            for (var i = 0; i < n; i++)
            {
                // NextDouble returns a random number between 0 and 1.
                // ... It is equivalent to Math.random() in Java.
                int r = i + (int)(random.NextDouble() * (n - i));
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }
    }
}
