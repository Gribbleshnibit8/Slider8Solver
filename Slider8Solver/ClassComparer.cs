using System.Collections.Generic;

namespace Slider8Solver
{
    class ClassComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x < y)
                return -1;
            return 1;
        }
    }
}
