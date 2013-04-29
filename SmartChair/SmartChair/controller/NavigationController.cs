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
        private Weightcontrol _wc;
        private PageExtended _lastPage;
        private TabItem _weightTab;
        TabItem _cogTab;
        TabItem _marbleTab;


        public void InitTabs(TabControl tabControl)
        {
            _weightTab = new TabItem();
            _weightTab.Header = "Weight";
            _cogTab = new TabItem();
            _cogTab.Header = "Center of Gravity";
            _marbleTab = new TabItem();
            _marbleTab.Header = "Marble";
            tabControl.Items.Add(_weightTab);
            tabControl.Items.Add(_cogTab);
            tabControl.Items.Add(_marbleTab);
        }

        public NavigationController(DataController dataController)
        {
            _dataController = dataController;
            _wc = new Weightcontrol();
            _dataController.AddSensorDataListener(_wc);
        }

        public void navigateCOG(Frame _frame)
        {
            removeListener();

            CenterGravity cog = new CenterGravity();
            _dataController.AddSensorDataListener(cog);
            _cogTab.Content = cog;
            _frame.Navigate(cog);
            _lastPage = cog;
        }

        public void navigateWeight(Frame _frame)
        {
            removeListener();
            _frame.Navigate(_wc);
            _lastPage = _wc;
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

        public void Navigate(TabItem ti)
        {
            Frame f = new Frame();

            if (ti.Equals(_weightTab))
            {
                navigateWeight(f);
            }
            else if(ti.Equals(_cogTab))
            {
                
                navigateCOG(f);
            }
            else if (ti.Equals(_marbleTab))
            {
                
            }

            ti.Content = f;
        }
    }
}
