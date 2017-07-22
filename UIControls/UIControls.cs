using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Physics;
namespace UIControls
{
    public class UIControls : IEnumerable<IControlItem>
    {
        private List<IControlItem> _controls;
		public UIControls(){this._controls = new List<IControlItem>();}
        public IControlItem this[int key]{get{return this._controls[key];}set{this._controls[key] = value;}}
        public List<IControlItem> ControlSet {get{return this._controls;}set {/*Nothing intended*/}}

        public void Add(CONTROL_PAGE catogory, Control control){
            _controls.Add(/**/new ControlItem(catogory, control, _controls.Count)/**/);
        }

        public List<BOX> GetBoxList()
        {
            List<BOX> boxes = new List<BOX>();
            foreach(IControlItem control in _controls)boxes.Add(control.BOX);
            return boxes;
        }

        public int RemoveAt(int index, object sender)
        {
            if (index >= _controls.Count) throw new IndexOutOfRangeException(_controls.ToString() +
                                                                                 ": Out of range exception");
            var form = sender as Form;
            string controlName = _controls[index].myControl.Name.ToString();
            form.Controls.RemoveByKey(controlName);
            _controls.RemoveAt(index);
            return _controls.Count;
        }
        private int FindControlIndex(string key)
        {
            int index = 0;
            foreach(IControlItem control in _controls)
            {
                if (control.myControl.Name == key) return index;
                index++;
            }
            throw new KeyNotFoundException("There are no controls with the name: " + key);
        }
        /// <summary>
        /// Removes Control from Form and IControlItem List and returns the number of elements after the operation
        /// </summary>
        /// <param name="control">Simply pass the control you wish to remove    sender: Form from which the control resides</param>
        public int RemoveControl(Control control, object sender)
        {
            var form = sender as Form;
            int index = -1;
            foreach (IControlItem controlBeingChecked in _controls)
            {
                if(controlBeingChecked.myControl.Name == control.Name)
                {
                    try{form.Controls.RemoveByKey(control.Name);
                        index = FindControlIndex(control.Name);
                        break;
                    }
                    catch (KeyNotFoundException ex){ MessageBox.Show(ex.Message);}
                }
            }
            _controls.RemoveAt(index);
            return _controls.Count;
        }

        public IEnumerator<IControlItem> GetEnumerator(){return _controls.GetEnumerator();}
        IEnumerator IEnumerable.GetEnumerator(){throw new NotImplementedException();}
    }
}
