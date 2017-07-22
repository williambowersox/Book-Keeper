using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System;
using System.Data.Entity;
using System.Collections.Generic;

namespace SqlServer
{
    [TestClass]
    public class SQLTESTS
    {
        [TestMethod]
        public void Constructor()
        {
            TestClass.test();
            

            string[] tableNames =  {"users",
                                 "business",
                                 "Cash Debit Book",
                                 "Cash Credit Book"};

            SqlController TEST = new SqlController(tableNames);
            
            TEST.ChangeField(0, 0, 1, "IT WORKS!", true);
            TEST.ChangeField("users", 0, 1, "William",true);
            TEST.InsertRow("users", new string[] { "COMPLETE","COMPLETE","COMPLETE"},true);
            TEST.DeleteRow("users", 2);

            try
            {
                MessageBox.Show(TEST.CommitToDatabase());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            //Assert.AreEqual(DataSet_EX.ActionType.CHANGE, (DataSet_EX.ActionType)tables._transactionType[0]);
            //Assert.AreEqual(DataSet_EX.ActionType.CHANGE, (DataSet_EX.ActionType)tables._transactionType[1]);
            //Assert.AreEqual(DataSet_EX.ActionType.ADD,    (DataSet_EX.ActionType)tables._transactionType[2]);
            //Assert.AreEqual(DataSet_EX.ActionType.DELETE, (DataSet_EX.ActionType)tables._transactionType[3]);


        }
    }
}
