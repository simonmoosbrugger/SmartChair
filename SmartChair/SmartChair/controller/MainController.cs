﻿using SmartChair.db;
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
            _dataController = new WiiController();
            _personController = new PersonController();
            _dbController = new SqlLiteController();

            List<Person> persons = _personController.getPersons();
            _personController.CurrentPerson = persons[0];
        }
    }
}
