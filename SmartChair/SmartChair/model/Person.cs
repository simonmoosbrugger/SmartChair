﻿using SmartChair.controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
            MainController c = MainController.Controller;
            string sql = "SELECT * from Person where ID = " + ID;
            DataTable dt = c.getDBController().Select(sql);

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
