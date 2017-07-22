using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace SmallBussiness.Suite
{
    public partial class HOME : Form
    {
        private UIControls.UIControls controls = new UIControls.UIControls();

        public HOME()
        {
            InitializeComponent();

        }

        private void HOME_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_SmallBussiness_userDataSet.users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this._SmallBussiness_userDataSet.users);

            //controls.Add(UIControls.CONTROL_PAGE.SPLASH, txt_FIRST);
            //controls.Add(UIControls.CONTROL_PAGE.SPLASH, btn_FRIST);

            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        private BindingSource bindingSource = new BindingSource();
        private void button1_Click(object sender, EventArgs e)
        {
            //List<DataRow> rows = this.usersTableAdapter.GetData().ToList<DataRow>();
            //DataTable table = (DataTable)dataGridView1.DataSource;
            //DataRowCollection rows = table.Rows;

            //foreach (DataRow row in rows)
            //{
            //    MessageBox.Show(row[1].ToString());
            //}

            chart1.Series["test1"].Points.AddXY(1,10);
            chart1.Series["test2"].Points.AddXY(2,25);
            chart1.Series["test1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["test2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["test1"].Color = Color.Red;
            chart1.Series["test2"].Color = Color.Blue;
        }
    }
}
