using SmartChair.gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SmartChair.controller
{
    public class NavigationController
    {
        private DataController _dataController;
        private Frame _frame;
        private Weightcontrol _wc;
        private PageExtended _lastPage;

        public NavigationController(DataController dataController)
        {
            _dataController = dataController;
            _wc = new Weightcontrol();
            _dataController.AddSensorDataListener(_wc);
        }

        public void setFrame(Frame frame)
        {
            _frame = frame;
        }

        public void navigateCOG()
        {
            removeListener();
            CenterGravity cog = new CenterGravity();
            _dataController.AddSensorDataListener(cog);
            _frame.Navigate(cog);
            _lastPage = cog;
        }

        public void navigateWeight()
        {
            removeListener();
            _frame.Navigate(_wc);
            _lastPage = _wc;
        }

        public void navigateStart()
        {
            removeListener();
            _frame.Navigate(new StartPage());
        }

        public void navigateBattery()
        {
            removeListener();
            BatteryTest bt = new BatteryTest();
            _dataController.AddBatteryStatListener(bt);
            _frame.Navigate(bt);
            _lastPage = bt;
        }

        private void removeListener()
        {
            if (_lastPage != null && _lastPage is DataController.SensorDataListener && _lastPage.RemoveListener() == true)
            {
                _dataController.RemoveSensorDataListener((DataController.SensorDataListener)_lastPage);
            }

            if (_lastPage != null && _lastPage is DataController.BatteryStatListener && _lastPage.RemoveListener() == true)
            {
                _dataController.RemoveBatteryStatListener((DataController.BatteryStatListener)_lastPage);
            }
        }
    }
}
