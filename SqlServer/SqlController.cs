using System.Data;
using System.Data.SqlClient;


namespace SqlServer
{
    public class SqlController
    {
        public SqlController(string[] tables, string connectionString = "")
        {
            string select = BuildFillString(tables);
            _adapter = new SqlDataAdapter(select, _connectionString);

            _adapter.Fill(_dataset);
            if (_dataset.Tables.Count != tables.Length) throw new DataException("Number of tables and number of table's names do not macth in: \r" + _connectionString);

            for (int i = 0; i < _dataset.Tables.Count; i++) { _dataset.Tables[i].TableName = tables[i]; }
        }
        public void ChangeField(int tableIndex, int rowIndex, int fieldIndex, string fieldValue, bool hasPrimaryKey = false)
            {this.DataSet.ChangeField(tableIndex, rowIndex, fieldIndex, fieldValue, hasPrimaryKey);}
        public void ChangeField(string tableName, int rowIndex, int fieldIndex, string fieldValue, bool hasPrimaryKey = false)
            {this.DataSet.ChangeField(tableName, rowIndex, fieldIndex, fieldValue, hasPrimaryKey);}
        public void DeleteRow(string tableName, int rowIndex)
            {this.DataSet.DeleteRow(tableName, rowIndex);}
        public void InsertRow(string tableName, string[] fields, bool hasPrimaryKey = false)
            {this.DataSet.InsertRow(tableName, fields, hasPrimaryKey);}
        public string CommitToDatabase()
            {return DataSet.CommitToDatabase(this);}

        internal string DeleteRowFromDatabase(string sql)
        {
            try
            {
                connection = CreateConnection();
            }
            catch (System.Exception ex) { return ex.Message; }
            try
            {
                connection.Open();
                _adapter.DeleteCommand = new SqlCommand(sql, connection);
                _adapter.DeleteCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex) { return ex.Message; }
            finally { connection.Close(); }
            return "Delete Row Successful";
        }
        internal string InsertIntoDataBase(string sql)
        {
            try
            {
                connection = CreateConnection();
            }
            catch (System.Exception ex) { return ex.Message; }
            try
            {
                connection.Open();
                _adapter.InsertCommand = new SqlCommand(sql, connection);
                _adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex) { return ex.Message; }
            finally { connection.Close(); }
            return "Insert Successful";
        }
        internal string UpdateRowIntoDatabase(string sql)
        {
            try { connection = CreateConnection(); }
            catch (System.Exception ex) { return ex.Message; }
            try
            {
                connection.Open();
                _adapter.UpdateCommand = new SqlCommand(sql, connection);
                _adapter.UpdateCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex) { return ex.Message; }
            finally { connection.Close(); }
            return "Update Successful";
        }
        internal string BuildFillString(string[] tables)
        {
            string select_string = "";
            foreach (string table in tables)
            {
                select_string += "SELECT * FROM [" + table + "]; ";
            }
            return select_string;
        }


        private string _connectionString = "Data Source= (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Users\\STRAT\\Documents\\GitHub\\SmallBussiness.Suite\\SmallBussiness.Suite\\SmallBussiness.Suite.mdf;Integrated Security = True; Connect Timeout = 30";
        private SqlConnection connection;
        private SqlDataAdapter _adapter;
        private SqlDataAdapter Adapter { get { return _adapter; } set { } }
        private DataSet_EX _dataset = new DataSet_EX();
        private DataSet_EX DataSet { get { return _dataset; } set { } }
        private SqlConnection CreateConnection() { return new SqlConnection(_connectionString); }
    }
}
