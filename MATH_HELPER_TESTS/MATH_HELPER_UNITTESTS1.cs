using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MATH_HELPER
{
    [TestClass]
    public class MATH_HELPER_UNIT_TESTS1
    {
        [TestMethod]
        public void Percent_Of_Relative_Change_TEST()
        {
            //Arange
            float delta1 =  60f;
            float delta2 =  15f;
            float delta3 =  10f;
            float delta4 =   5f;
            float delta5 =   0f;
            float delta6 =  -5f;
            float delta7 = -10f;
            float delta8 = -15f;
            float delta9 = -60f;


            //Act
            float change1 = MATH_HELPER.Delta_Change(10.0f, delta1);
            float change2 = MATH_HELPER.Delta_Change(10.0f, delta2);
            float change3 = MATH_HELPER.Delta_Change(10.0f, delta3);
            float change4 = MATH_HELPER.Delta_Change(10.0f, delta4);
            float change5 = MATH_HELPER.Delta_Change(10.0f, delta5);
            float change6 = MATH_HELPER.Delta_Change(10.0f, delta6);
            float change7 = MATH_HELPER.Delta_Change(10.0f, delta7);
            float change8 = MATH_HELPER.Delta_Change(10.0f, delta8);
            float change9 = MATH_HELPER.Delta_Change(10.0f, delta9);

            //Assert
            Assert.AreEqual(  50, change1, .00001);
            Assert.AreEqual(   5, change2, .00001);
            Assert.AreEqual(   0, change3, .00001);
            Assert.AreEqual(  -5, change4, .00001);    
            Assert.AreEqual( -10, change5, .00001);
            Assert.AreEqual( -15, change6, .00001);
            Assert.AreEqual( -20, change7, .00001);
            Assert.AreEqual( -25, change8, .00001);
            Assert.AreEqual( -70, change9, .00001);

        }
    }
}
