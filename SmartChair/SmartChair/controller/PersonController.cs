using SmartChair.db;
using SmartChair.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChair.controller
{
    public class PersonController
    {
        DbController _dbController;
        Person _CurrentPerson;
        List<string> _Columns = new List<string>();

        public List<Person> getPersons()
        {
            string sql = "SELECT * from Person";
            DataTable dt = _dbController.Execute(sql);
            List<Person> list = new List<Person>();
            foreach (DataRow row in dt.Rows)
            {
                long ID = (long)row["ID"];
                string fname = row["Fname"].ToString();
                string lname = row["Lname"].ToString();
                list.Add(new Person(ID, fname, lname));
            }
            return list;
        }

        public Person CurrentPerson
        {
            get
            {
                return _CurrentPerson;
            }

            set
            {
                _CurrentPerson = value;
            }
        }

        public Person GetPersonByName(string WindowsUserName)
        {
            string sql = "SELECT * from Person WHERE Fname = '" + GetFirstname(WindowsUserName) + "' AND Lname='" + GetLastName(WindowsUserName) + "'";
            DataTable dt = _dbController.Execute(sql);
            Person p = null;
            foreach (DataRow row in dt.Rows)
            {
                long ID = (long)row["ID"];
                string fname = row["Fname"].ToString();
                string lname = row["Lname"].ToString();
                p = new Person(ID, fname, lname);
            }
            return p;

        }

        public PersonController(DbController dbController)
        {
            _Columns.Add("Fname");
            _Columns.Add("Lname");
            _dbController = dbController;
        }

        public void AddUser(string Fname, string Lname)
        {
            List<object> Values = new List<object>();
            Values.Add(Fname);
            Values.Add(Lname);
            _dbController.Insert("Person", _Columns, Values);
        }

        public bool UserExists(string windwosUsername)
        {
            string[] arr = windwosUsername.Split(' ');
            string fname = arr[0];
            string lname;
            if (arr.Count<string>() < 2)
            {
                lname = "";
            }
            else
            {
                lname = arr[1];
            }

            foreach (Person p in getPersons())
            {
                if (p.Firstname == fname && p.Lastname == lname)
                {
                    return true;
                }

            }
            return false;
        }

        public bool UserExists(string fname, string lname)
        {
            foreach (Person p in getPersons())
            {
                if (p.Firstname == fname && p.Lastname == lname)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetFirstname(string WindowsUserName)
        {
            string[] split = WindowsUserName.Split(' ');
            return split[0];

        }

        public string GetLastName(string WindowsUserName)
        {
            string[] split = WindowsUserName.Split(' ');

            if (split.Length < 2)
            {
                return "";
            }
            return split[1];
        }

        public Person GetPersonByName(string firstname, string lastname)
        {
            string sql = "SELECT * from Person WHERE Fname = '" + firstname + "' AND Lname='" + lastname + "'";
            DataTable dt = _dbController.Execute(sql);
            Person p = null;
            foreach (DataRow row in dt.Rows)
            {
                long ID = (long)row["ID"];
                string fname = row["Fname"].ToString();
                string lname = row["Lname"].ToString();
                p = new Person(ID, fname, lname);
            }
            return p;

        }
    }
}
