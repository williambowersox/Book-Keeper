using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Forms;


namespace SqlServer
{
    public static class TestClass
    {

        public static void test()
        {
            TestModel test = new TestModel();

            var query = from user in test.users
                        select user;
            List<user> users = query.ToList();

            foreach (var user in users)
                MessageBox.Show(user.firstName);

        }
    }
}