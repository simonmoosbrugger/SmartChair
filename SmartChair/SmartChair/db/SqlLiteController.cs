using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;


namespace SmartChair.db
{
    class SqlLiteController : DbController
    {
        string _DataSource = "SmartChairDB.db";
        string _CreateScript = @"db/DbCreate.sql";
        SQLiteConnection _Connection;


        public SqlLiteController()
        {
            Init();
            CreateTables();
        }

        public void Init()
        {
            _Connection = new SQLiteConnection();
            _Connection.ConnectionString = "Data Source=" + _DataSource;
            _Connection.Open();
        }

        public void CreateTables()
        {
            string createTable = System.IO.File.ReadAllText(_CreateScript);
            SQLiteCommand command = new SQLiteCommand(_Connection);
            command.CommandText = createTable;
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public DataTable Select(string Query)
        {
            SQLiteCommand command = new SQLiteCommand(_Connection);
            command.CommandText = Query;
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            return dt;
        }

        public Object SelectScalar(string Query)
        {
            SQLiteCommand command = new SQLiteCommand(_Connection);
            return command.ExecuteScalar();
        }

        public void Insert(string Table, List<string> ColumnNames, List<string> Values)
        {
            throw new NotImplementedException();
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
