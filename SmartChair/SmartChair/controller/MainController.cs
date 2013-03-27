using SmartChair.db;
using SmartChair.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SmartChair.controller
{
    public class MainController
    {
        private static MainController _c;
        private PersonController _personController;
        private DataController _dataController;
        private DbController _dbController;

        public PersonController PersonController
        {
            get { return _personController; }
        }

        public DataController DataController
        {
            get { return _dataController; }
        }
        
        public DbController DbController
        {
            get { return _dbController; }
        }

        public static MainController Controller
        {
            get {
                if (_c == null)
                {
                    _c = new MainController();                    
                }
                return MainController._c; 
            }
        }

        private MainController()
        {
            _dbController = new SqlLiteController();
            _dataController = new TestDataController();
            _personController = new PersonController(_dbController);

            List<Person> persons = _personController.getPersons();
            try
            {
                _personController.CurrentPerson = persons[0];
            }
            catch (Exception)
            {
                string testDataScript = @"db\DbData.sql";
                string sql = File.ReadAllText(testDataScript);
                _dbController.Execute(sql);
                persons = _personController.getPersons();
                _personController.CurrentPerson = persons[0];
            }
        }
    }
}
