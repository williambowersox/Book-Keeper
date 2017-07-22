using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;



namespace SqlServer
{
    public class DataSet_EX : DataSet
    {
        public void InsertRow(string tableName, string[] fields, bool hasPrimaryKey = false)
        {
            //Declare
            int[] rowCount = new int[2];
            int tableIndex = _FindTableIndexByName(tableName);
            int newID = _setInsertID(tableIndex);

            //Validate
            _FieldCountMatch(tableIndex, fields);//Throw an exception if string array is not the proper length;

            //Set up
            DataRow newRow = _SetInsertFields(Tables[0].NewRow(), fields, newID);
            List<object> newRowObjects = _GetListOfFields(newRow);

            //Record
            _addedRows.Add(new _Row<object>(tableName, newRowObjects));
            this.Tables[tableIndex].Rows.InsertAt(newRow, this.Tables[tableIndex].Rows.Count);

            _addTransactionType(ActionType.ADD, hasPrimaryKey);

            //Report
            rowCount[1] = this.Tables[tableIndex].Rows.Count;
            if (rowCount[0] == rowCount[1]) throw new Exception("Insert Row failed");
        }
        public void DeleteRow(string tableName, int rowIndex)
        {
            int tableIndex = _FindTableIndexByName(tableName);

            DataRow deleteRow = this.Tables[tableIndex].Rows[rowIndex];
            List<object> deleteObjects = _GetListOfFields(deleteRow);
            this._deletedRows.Add(new _Row<object>(tableName, deleteObjects));

            this.Tables[tableIndex].Rows.RemoveAt(rowIndex);
            _addTransactionType(ActionType.DELETE, false);

        }
        public void ChangeField(int tableIndex, int rowIndex, int fieldIndex, string fieldValue, bool hasPrimaryKey = false)
        {
            ///Insert the old row into _chagedRow for searching purposes when commiting
            ///to the database;
            int[] indexs = _FieldIndexArray(tableIndex, rowIndex, fieldIndex);
            string tableName = Tables[indexs[0]].TableName;

            DataRow oldRow = Tables[indexs[0]].Rows[indexs[1]];
            var oldFields = _GetListOfFields(oldRow) as List<object>;

            Tables[indexs[0]].Rows[indexs[1]].BeginEdit();
            Tables[indexs[0]].Rows[indexs[1]].SetField<string>(indexs[2], fieldValue);
            Tables[indexs[0]].Rows[indexs[1]].EndEdit();


            DataRow newRow = Tables[indexs[0]].Rows[indexs[1]];
            var newFields = _GetListOfFields(newRow) as List<object>;


            _changedRow.Add(new _RowTuple(tableName, oldFields, newFields));
            _addTransactionType(ActionType.CHANGE, hasPrimaryKey);

        }
        public void ChangeField(string tableName, int rowIndex, int fieldIndex, string fieldValue, bool hasPrimaryKey = false)
        {
            int tableIndex = _FindTableIndexByName(tableName);
            ChangeField(tableIndex, rowIndex, fieldIndex, fieldValue, hasPrimaryKey);

        }
        public string CommitToDatabase(SqlController sqlcontroller)
        {
            string sql = "";
            int[] TCount = GetCounts();
            int sum = TCount[0] + TCount[1] + TCount[2];

            if (sum != _transactionType.Count) throw new Exception("TransactionType.Count does not equal sum");

            foreach (_TransactionType t in _transactionType)
            {
                ActionType key = t.ACTIONTYPE;
                bool primaryProcessed = true;
                if (t.HASPRIMARYKEY == HasPrimaryKey.YES) {primaryProcessed = false;}

                switch (key)
                {
                    case ActionType.ADD:
                        {

                            sql += "INSERT INTO " + _addedRows[0].TABLENAME +
                                                        " VALUES(";

                            foreach(object field in _addedRows[0].FIELDS)
                            {
                                if (primaryProcessed)
                                {
                                    Type test = typeof(string);
                                    if (field.GetType() == test)
                                    {
                                        sql += "'";
                                    }
                                    sql += $"{field.ToString()}";
                                    if (field.GetType() == test)
                                    {
                                        sql += "'";
                                    }
                                    sql += ",";
                                }
                                primaryProcessed = true;
                            }

                            int removeIndex = sql.Length - 1;
                            sql = sql.Remove(removeIndex, 1);
                            sql += ");";

                            try
                            {
                                sqlcontroller.InsertIntoDataBase(sql);
                                _addedRows.RemoveAt(0);
                            }
                            catch(Exception ex)
                            {
                                return ex.Message;
                            }
                            sql = "";
                            break;
                        }

                    case ActionType.CHANGE:
                        {
                            DataRow row = Tables[_changedRow[0].OLDROW.TABLENAME].NewRow();
                            DataColumnCollection columns = row.Table.Columns;
                            bool primaryProcessedStep2 = true;
                            for (int i = 0; i<columns.Count; i++)
                            {
                                
                                if (t.HASPRIMARYKEY == HasPrimaryKey.YES && primaryProcessed == false)
                                {
                                    primaryProcessedStep2 = false;
                                    primaryProcessed = true;
                                    continue;
                                }

                                sql += $"UPDATE [{_changedRow[0].NEWROW.TABLENAME}]  SET [{columns[i].ColumnName}] =";
                                Type columnType = _changedRow[0].NEWROW.FIELDS[i].GetType();
                                if (columnType == typeof(string)) sql += "'";
                                sql += $"{_changedRow[0].NEWROW.FIELDS[i]}";
                                if (columnType == typeof(string)) sql += "'";

                                sql += " WHERE ";

                                int valueIndex = 0;
                                foreach (DataColumn column in columns)
                                {
                                    if (primaryProcessedStep2 == true)
                                    {
                                        columnType = _changedRow[0].NEWROW.FIELDS[valueIndex].GetType();
                                        sql += $"[{column.ColumnName}] = ";
                                        if (columnType == typeof(string)) sql += "'";
                                        sql += $"{_changedRow[0].OLDROW.FIELDS[valueIndex].ToString()}";
                                        if (columnType == typeof(string)) sql += "'";
                                        sql += " AND ";
                                    }
                                    valueIndex++;
                                    primaryProcessedStep2 = true;
                                }
                                int removeIndex = sql.Length - 5;
                                sql = sql.Remove(removeIndex,5);
                                sql += ";\r";
                            }
                            try
                            {
                                sqlcontroller.UpdateRowIntoDatabase(sql);
                                _changedRow.RemoveAt(0);
                            }catch(Exception ex) { return ex.Message; }
                            finally { sql = ""; }
                            break;
                        }
                    case ActionType.DELETE:
                        {
                            sql += $"DELETE FROM [{_deletedRows[0].TABLENAME}] WHERE ";

                            DataRow row = Tables[_deletedRows[0].TABLENAME].NewRow();
                            DataColumnCollection columns = row.Table.Columns;
                            int valueIndex = 0;
                            foreach (DataColumn column in columns)
                            {
                                Type columnType = column.DataType;
                                sql += $"[{column.ColumnName}] = ";
                                if (columnType == typeof(string)) sql += "'";
                                sql += $"{_deletedRows[0].FIELDS[valueIndex]}";
                                if (columnType == typeof(string)) sql += "'";
                                sql += " AND ";
                                valueIndex++;
                            }
                            int removeIndex = sql.Length - 5;
                            sql = sql.Remove(removeIndex, 5);
                            sql += ";\r";
                            try
                            {
                                sqlcontroller.DeleteRowFromDatabase(sql);
                                _deletedRows.RemoveAt(0);
                            }
                            catch (Exception ex) { return ex.Message; }
                            finally { sql = ""; }
                            break;
                        }

                }
            }_transactionType.Clear();
            return sql;

        }


        //private support functions
        private int[] _FieldIndexArray(int tableIndex, int rowIndex, int fieldIndex)
        {
            int[] indexs = { tableIndex, rowIndex, fieldIndex };
            return indexs;
        }
        private int _FindTableIndexByName(string tableName)
        {
            for (int i = 0; i < this.Tables.Count; i++)
                if (this.Tables[i].TableName == tableName)return i;
            return -1;
        }
        private List<object> _GetListOfFields(DataRow row)
        {
            List<object> builder = new List<object>();
            foreach(object field in row.ItemArray.AsEnumerable()) builder.Add(field);
            return builder;
        }
        private void _addTransactionType(ActionType action, bool hasPrimaryKey)
        {
            if (hasPrimaryKey) _transactionType.Add(new _TransactionType(action, HasPrimaryKey.YES));
            else _transactionType.Add(new _TransactionType(action, HasPrimaryKey.NO));
        }
        private void _FieldCountMatch(int tableIndex, string[] fields)
        {
            int fieldCount = fields.Count();
            if (this.Tables[tableIndex].Rows[0].ItemArray.Length - 1 != fieldCount)
            {
                throw new Exception("Field counts do not match");
            }
        }
        private int _setInsertID(int tableIndex)
        {
            var query = (from t in Tables[tableIndex].AsEnumerable()
                         orderby t[0] descending
                         select t[0]).Take(1).ToList();
            int newID;
            int.TryParse(query[0].ToString(), out newID);
            return newID;
        }
        private DataRow _SetInsertFields(DataRow newRow, string[] fields,int newID)
        {

            newRow.SetField(0, newID.ToString());
            {
                int i = 1;
                foreach (string str in fields)
                {
                    newRow.SetField(i, str);
                    i++;
                }
            }
            return newRow;
        }
        private int[] GetCounts()
        {
            int[] counts = { _changedRow.Count, _deletedRows.Count, _addedRows.Count };
            return counts;
        }


        //private vars and support classes
        private enum ActionType { ADD, CHANGE, DELETE };
        private enum HasPrimaryKey { NO, YES };
        private List<_RowTuple> _changedRow = new List<_RowTuple>();
        private List<_Row<object>> _deletedRows = new List<_Row<object>>();
        private List<_Row<object>> _addedRows = new List<_Row<object>>();
        private List<_TransactionType> _transactionType = new List<_TransactionType>();
        private class _RowTuple
        {
            public _RowTuple(string tableName, List<object> oldRow, List<object> newRow)
            {
                _oldRow = new _Row<object>(tableName,
                                     oldRow);
                _newRow = new _Row<object>(tableName,
                                    newRow);
            }
            private _Row<object> _oldRow;
            private _Row<object> _newRow;
            public _Row<object> OLDROW { get { return _oldRow; } set { } }
            public _Row<object> NEWROW { get { return _newRow; } set { } }
        }
        private class _Row<T>
        {
            private List<T> _fields = new List<T>();
            public List<T> FIELDS { get { return _fields; } set { } }
            private _Row() { }
            public _Row(string tableName, List<object> fields)
            {
                TABLENAME = tableName;
                foreach(object field in fields) _fields.Add((T)field);
            }
            public string TABLENAME { get; set; }
        }
        private class _TransactionType
        {
            private int _actiontype;
            public ActionType ACTIONTYPE { get { return (ActionType)_actiontype; } set { } }
            private int _hasprimarykey;
            public HasPrimaryKey HASPRIMARYKEY { get { return (HasPrimaryKey)_hasprimarykey; } set { } }

            private _TransactionType() { }
            public _TransactionType(ActionType actionType, HasPrimaryKey hasprimarykey)
            {
                _actiontype = (int)actionType;
                _hasprimarykey = (int)hasprimarykey;
            }
        }
    }
}
