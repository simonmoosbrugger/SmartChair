using SmartChair.controller;
using System.Data;

namespace SmartChair.model
{
    public class Person
    {
        private long _ID;
        private string _Fname;
        private string _Lname;

        public string Firstname
        {
            get { return _Fname; }
            set { _Fname = value; }
        }

        public string Lastname
        {
            get { return _Lname; }
            set { _Lname = value; }
        }

        public long ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Person(long ID)
        {
            string sql = "SELECT * from Person where ID = " + ID;
            DataTable dt = MainController.Controller.DbController.Execute(sql);
        }

        public Person(string Fname, string Lname)
        {
            _Fname = Fname;
            _Lname = Lname;
        }

        public Person(long ID, string Fname, string Lname)
        {
            _Fname = Fname;
            _Lname = Lname;
            _ID = ID;
        }
    }
}
