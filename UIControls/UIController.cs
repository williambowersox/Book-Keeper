using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIControls
{
    /// <summary>
    /// UIController is used to seperate and maintian the UI from the logic of the code.
    /// </summary>
    public static class UIController
    {

        public static int RemoveControl(UIControls controls, Control controlToRemove, Form form){
            controls.RemoveControl(controlToRemove, form);
            return controls.Count();
        }
        public static void DisableUI(UIControls controls){
            foreach (IControlItem control in controls) {
                control.myControl.Enabled = false;
            }
        }
        public static void DisableUI(Type controlType, UIControls controls) {
            Type type;
            foreach (ControlItem control in controls) {
                type = control.myControl.GetType();
                if (type == controlType) {
                    control.myControl.Enabled = false;
                }
            }
        }
        public static void DisableUI(CONTROL_PAGE page, UIControls controls){
            foreach (IControlItem control in controls){
                if (control.catogory == page){
                    control.myControl.Enabled = false;
                }
            }
        }
        public static void DisableUI(CONTROL_PAGE page, Type controlType, UIControls controls){
            Type type;
            foreach (IControlItem control in controls){
                type = control.myControl.GetType();
                if (type == controlType && control.catogory == page){
                    control.myControl.Enabled = false;
                }
            }
        }

        ////////////////////////////////////////////////////////
        public static void EnableUI(UIControls controls)
        {
            foreach (ControlItem control in controls)
            {
                control.myControl.Enabled = true;
            }
        }
        public static void EnableUI(Type controlType, UIControls controls)
        {
            Type type;
            foreach (ControlItem control in controls)
            {
                type = control.myControl.GetType();
                if (type == controlType)
                {
                    control.myControl.Enabled = true;
                }
            }
        }
        public static void EnableUI(CONTROL_PAGE page, UIControls controls)
        {
            foreach (ControlItem control in controls)
            {
                if (control.catogory == page)
                {
                    control.myControl.Enabled = true;
                }
            }
        }
        public static void EnableUI(CONTROL_PAGE page, Type controlType, UIControls controls)
        {
            Type type;
            foreach (ControlItem control in controls)
            {
                type = control.myControl.GetType();
                if (type == controlType && control.catogory == page)
                {
                    control.myControl.Enabled = true;
                }
            }
        }

        public static void ScaleControl(Control control, float factor){control.Scale(new SizeF(factor, factor));}
        public static void ScaleAllControls(UIControls controls, SizeF factor) {foreach (IControlItem control in controls as UIControls)control.myControl.Scale(factor);}
        public static void AdjustEverything(UIControls controls, Form form, ref Size formBeforeSize)
        {
            Size formAfterSize = form.Size;

            if (formBeforeSize.Height == 0 || formBeforeSize.Width == 0) throw new DivideByZeroException("Attempted to divide by zero");

            float factorHeight = (float)formAfterSize.Height / (float)formBeforeSize.Height;
            float factorWidth = (float)formAfterSize.Width / (float)formBeforeSize.Width;

            SizeF change = new SizeF(factorWidth, factorHeight);
            ScaleAllControls(controls, change);
            MoveRelativeToSizeChange(controls, change);
        }

        public static void MoveRelativeToSizeChange(UIControls controls, SizeF change)
        {
            Point newLoc = new Point();
            foreach(IControlItem control in controls as UIControls)
            {
                newLoc.X = (int)(control.myAnchor.X * (float)change.Width);
                newLoc.Y = (int)(control.myAnchor.Y * (float)change.Height);
                control.myControl.Location = newLoc;
            }
        }
    }
}
