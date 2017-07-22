using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Physics
{
    public struct BOX
    {
        public BOX(Point topleft, Size size)
        {
            TopLeft = topleft;
            Size = size;
        }
        public Point TopLeft { get; set; }
        public Size Size { get; set; }
    }
}
