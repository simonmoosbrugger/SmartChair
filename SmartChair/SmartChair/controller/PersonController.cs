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
        Person _CurrentPerson;
        List<string> _Columns = new List<string>();

        public List<Person> getPersons()
        {
            string sql = "SELECT * from Person";
            DataTable dt = MainController.Controller.DbController.Select(sql);
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

        public PersonController()
        {
            _Columns.Add("Fname");
            _Columns.Add("Lname");
        }

        public void AddUser(string Fname, string Lname)
        {
            List<object> Values = new List<object>();
            Values.Add(Fname);
            Values.Add(Lname);
            MainController.Controller.DbController.Insert("Person", _Columns, Values);
        }
    }
}
