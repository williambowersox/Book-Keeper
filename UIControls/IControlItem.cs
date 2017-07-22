using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Physics;
namespace UIControls
{
    public interface IControlItem
    {
        CONTROL_PAGE catogory { get; }
        Control myControl { get; }
        Point myAnchor { get; set; }
        Point myLoc { get; set; }
        Point MoveTo(Point change);
        SizeF Size { get; set; }
        BOX BOX { get; }
    }
}
