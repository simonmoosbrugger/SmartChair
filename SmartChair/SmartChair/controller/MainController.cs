
using SmartChair.db;
using SmartChair.gui;
using SmartChair.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SmartChair.gui.controls.popup;
using System.Windows.Threading;

namespace SmartChair.controller
{
    public class MainController
    {
        private static MainController _c;
        private PersonController _personController;

        public Dispatcher _mainWindowDispatcher;

        private DataController _dataController;
        private DbController _dbController;
        private NavigationController _navigationController;

        private NotifyMessageManager _notifyMessageMgr;

        public Dispatcher MainWindowDispatcher
        {
            get { return _mainWindowDispatcher; }
            set { _mainWindowDispatcher = value; }
        }

        public Person CurrentPerson
        {
            get { return _personController.CurrentPerson; }
        }

        public PersonController PersonController
        {
            get { return _personController; }
            set { _personController = value; }
        }
        public DataController DataController
        {
            get { return _dataController; }
        }

        public DbController DbController
        {
            get { return _dbController; }
        }

        public NavigationController NavigationController
        {
            get
            {
                if (_navigationController == null)
                {
                    _navigationController = new NavigationController(_dataController);
                }
                return _navigationController;
            }
        }

        public static MainController GetInstance
        {
            get
            {
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
            //_dataController = new WiiController();
            _personController = new PersonController(_dbController);

            _dataController.AddSensorDataListener(new MovementRecognitionController());

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

            _notifyMessageMgr = new NotifyMessageManager
                (
                    Screen.Width,
                    Screen.Height,
                    200,
                    150
                );
            //_notifyMessageMgr.Start();
        }

        public void EnqueNotificationMessage(NotifyMessage message)
        {
            _notifyMessageMgr.EnqueueMessage(message, _mainWindowDispatcher);
        }
    }
}
