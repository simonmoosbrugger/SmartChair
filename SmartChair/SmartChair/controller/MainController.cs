using SmartChair.db;
using SmartChair.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartChair.controller
{
    public class MainController
    {
        private static MainController _c;

        public static MainController Controller
        {
            get {
                if (_c == null)
                {
                    _c = new MainController();
                    List<Person> persons = PersonController.Instance.getPersons();
                    PersonController.Instance.CurrentPerson = persons[0];
                }
                return MainController._c; 
            }
        }

        private MainController()
        {
            
        }

        public DbController getDBController()
        {
            return SqlLiteController.Instance;
        }

        public PersonController getPersonController()
        {
            return PersonController.Instance;
        }
    }
}
