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
        }

        public void navigateWeight()
        {
            removeListener();
            _frame.Navigate(_wc);
        }

        public void navigateGame()
        {

        }

        private void removeListener()
        {
            if (_lastPage != null && _lastPage is DataController.SensorDataListener && _lastPage.RemoveListener() == true)
            {
                _dataController.RemoveSensorDataListener((DataController.SensorDataListener)_lastPage);
            }
        }
    }
}
