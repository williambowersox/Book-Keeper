using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Physics
{
    public static class Physics_Functions
    {
        /// <summary>
        /// Deterimes is two BOX objects intersects with each other.
        /// Assumes Quadrant 4
        /// </summary>
        public static bool Intersects(BOX A, BOX B)
        {
            if ((B.TopLeft.X <= A.TopLeft.X + A.Size.Width &&
                    B.TopLeft.X > A.TopLeft.X) &&
                    (B.TopLeft.Y <= A.TopLeft.Y + A.Size.Height &&
                    B.TopLeft.X > A.TopLeft.X))
                return true;
            return false;
        }
        public static bool AnyIntersects(List<BOX> boxes)
        {
            for (int i = 0; i < boxes.Count; i++)
                for (int j = 0; j < boxes.Count; j++) if (Physics.Physics_Functions.Intersects(boxes[i], boxes[j])
                                                             && i != j) return true;
            return false;
        }
    }
}
