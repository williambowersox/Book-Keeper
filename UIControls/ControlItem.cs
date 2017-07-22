using System;
using System.Drawing;
using System.Windows.Forms;
using Physics;
namespace UIControls
{
    public class ControlItem : IControlItem
    {
        private string CLASSNAME = "ControlItem";
        public int index { get; }
        private SizeF _size = new SizeF();
        public SizeF Size
        {
            get
            {
                return _size;
            }
            set
            {
                if (value.Width < 0 || value.Height < 0) throw new ArgumentOutOfRangeException(CLASSNAME + ": _size cannot have a value less than 0.");
                _size = value;
            }
        }
        public CONTROL_PAGE catogory { get; }
        public Control myControl { get; }
        private Point _myAnchor = new Point();
        public Point myAnchor
        {
            get { return _myAnchor; }
            set
            {
                if (value.X < 0) value.X = 0;
                if (value.Y < 0) value.Y = 0;
            }
        }
        public Point myLoc { get; set; }
        public BOX BOX {get{return new BOX(myLoc, new Size((int)Size.Width, (int)Size.Height));}}

        public ControlItem(CONTROL_PAGE type, Control control)
        {
            this.catogory = type;
            this.myControl = control;
            this._myAnchor = this.myControl.Location;
            this.Size = this.myControl.Size;
            this.myLoc = myControl.Location;
        }
        public ControlItem(CONTROL_PAGE type, Control control, int index) 
            : this(type,control){this.index = index;}

        /// <summary>
        /// Simple location change
        /// </summary>
        public Point MoveTo(Point change)
        {
            myControl.Location = new Point(change.X,change.Y);
            return myLoc = change;
        }
        public Point MoveRelative(float x, float y)
        {
            float deltaX, deltaY;
            deltaX = MATH_HELPER.MATH_HELPER.Delta_Change(myLoc.X, x);
            deltaY = MATH_HELPER.MATH_HELPER.Delta_Change(myLoc.Y, y);


            x = myLoc.X + deltaX;
            y = myLoc.Y + deltaY;

            myLoc = new Point((int)x, (int)y);
            myControl.Location = myLoc;

            return new Point((int)deltaX,(int)deltaY);
        }


    }
}
