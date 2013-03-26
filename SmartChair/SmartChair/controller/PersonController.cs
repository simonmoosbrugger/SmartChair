using SmartChair.db;
using SmartChair.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChair.controller
{
    class PersonController
    {
        private static PersonController _Instance;
        Person _CurrentPerson;
        List<string> _Columns = new List<string>();

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
