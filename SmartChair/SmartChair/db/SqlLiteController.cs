using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Diagnostics;
using SmartChair.controller;

namespace SmartChair.db
{
    public class SqlLiteController : DbController
    {
        string _DataSource = "SmartChairDB.db";
        string _CreateScript = @"db/DbCreate.sql";
        SQLiteConnection _Connection;

        public SqlLiteController()
        {
            Init();
            CreateTables();
        }

        private void Init()
        {
            _Connection = new SQLiteConnection();
            _Connection.ConnectionString = "Data Source=" + _DataSource;
            _Connection.Open();
        }

        private void CreateTables()
        {
            try
            {
                string createTable = System.IO.File.ReadAllText(_CreateScript);
                SQLiteCommand command = new SQLiteCommand(_Connection);
                command.CommandText = createTable;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public DataTable Execute(string Query)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(Query, _Connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }

        public Object SelectScalar(string Query)
        {
            SQLiteCommand command = new SQLiteCommand(_Connection);
            return command.ExecuteScalar();
        }

        public void Insert(string Table, List<string> ColumnNames, List<Object> Values)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO ");
            sb.Append(Table);
            sb.Append(" (");
            foreach (string column in ColumnNames)
            {
                sb.Append(column);
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(") VALUES (");

            foreach (string value in Values)
            {
                if (value.GetType() == typeof(string))
                {
                    sb.Append("'" + value + "',");
                }
                else
                {
                    sb.Append(value + ",");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            _Connection.Close();
            _Connection.Dispose();
        }
    }
}
