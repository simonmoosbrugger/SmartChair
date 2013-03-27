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
        private static PersonController _Instance;
        Person _CurrentPerson;
        List<string> _Columns = new List<string>();

        public List<Person> getPersons()
        {
            MainController c = MainController.Controller;
            string sql = "SELECT * from Person";
            DataTable dt = c.getDBController().Select(sql);
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

        public static PersonController Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PersonController();
                }
                return _Instance;
            }
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

        private PersonController()
        {
            _Columns.Add("Fname");
            _Columns.Add("Lname");
        }

        public void AddUser(string Fname, string Lname)
        {
            List<object> Values = new List<object>();
            Values.Add(Fname);
            Values.Add(Lname);
            SqlLiteController.Instance.Insert("Person", _Columns, Values);
        }
    }
}
