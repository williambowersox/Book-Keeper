using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Windows.Forms;

namespace UIControls

{
    [TestClass]
    public class UIControlsTESTS
    {
        
        [TestMethod]
        public void testRelativeMove()
        {
            //Arrage
            Button button = new Button();
            button.Size = new Size(10, 10);
            Assert.AreEqual(new Size(10, 10), button.Size);
            button.Location = new Point(10, 10);
            ControlItem Item = new ControlItem(CONTROL_PAGE.SPLASH, button);
           

            //Act
            Point testPoint1 = Item.MoveRelative(20, 20);
            Point testPoint2 = Item.MoveRelative(5,5);

            //Assert
            var expectPoint1 = new Point(10, 10);
            Assert.AreEqual(expectPoint1.X,testPoint1.X);
            Assert.AreEqual(expectPoint1.Y, testPoint1.Y);

            var expectPoint2 = new Point(-15, -15);
            Assert.AreEqual(expectPoint2.X, testPoint2.X);
            Assert.AreEqual(expectPoint2.Y, testPoint2.Y);
        }
        
    }
}
