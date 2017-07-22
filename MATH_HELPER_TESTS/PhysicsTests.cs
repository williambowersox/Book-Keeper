using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;

namespace Physics
{
    [TestClass]
    public class PhysicsTests
    {
        [TestMethod]
        public void CollisionTEST()
        {
            {//TEST 1
                BOX A, B;
                A = new BOX();
                B = new BOX();
                A.Size = new Size(10, 10);
                A.TopLeft = new Point(0, 0);
                B.Size = new Size(10, 10);
                B.TopLeft = new Point(5, 5);
                bool result = Physics_Functions.Intersects(A, B);
                Assert.AreEqual(result, true);
            }
            {//TEST 2
                BOX A, B;
                A = new BOX();
                B = new BOX();
                A.Size = new Size(10, 10);
                A.TopLeft = new Point(0, 0);
                B.Size = new Size(10, 10);
                B.TopLeft = new Point(15, 15);
                bool result = Physics_Functions.Intersects(A, B);
                Assert.AreEqual(result, false);
            }
        }


        [TestMethod]
        public void AnyCollisionTEST()
        {

            //Arrange
            List<BOX> boxes1 = new List<BOX>();
            boxes1.Add(new Physics.BOX(new Point(0, 0), new Size(10, 10)));
            boxes1.Add(new Physics.BOX(new Point(15, 15), new Size(10, 10)));
            boxes1.Add(new Physics.BOX(new Point(30, 30), new Size(10, 10)));
            boxes1.Add(new Physics.BOX(new Point(60, 60), new Size(10, 10)));

            List<BOX> boxes2 = new List<BOX>();
            boxes2.Add(new Physics.BOX(new Point(0, 0), new Size(10, 10)));
            boxes2.Add(new Physics.BOX(new Point(5, 5), new Size(10, 10)));
            boxes2.Add(new Physics.BOX(new Point(0, 0), new Size(10, 10)));
            boxes2.Add(new Physics.BOX(new Point(15, 15), new Size(10, 10)));

            //Act
            bool result1 = Physics.Physics_Functions.AnyIntersects(boxes1);
            bool result2 = Physics.Physics_Functions.AnyIntersects(boxes2);

            //Assert
            Assert.AreEqual(false, result1);
            Assert.AreEqual(true, result2);
        }
    }
}


