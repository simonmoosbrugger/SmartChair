using SmartChair.gui.popup;
using SmartChair.gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartChair.controller
{
    public class NavigationController
    {
        private DataController _dataController;
        private Weightcontrol _wc;
        private PageExtended _lastPage;
        TabItem _weightTab;
        TabItem _cogTab;
        TabItem _marbleTab;

        TabItem _weightTabSettings;
        TabItem _cogTabSettings;
        TabItem _marbleTabSettings;


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

        public void InitSettingsTabs(TabControl tabControl)
        {
            _weightTabSettings = new TabItem();
            _weightTabSettings.Header = "Weight";
            _cogTabSettings = new TabItem();
            _cogTabSettings.Header = "Center of Gravity";
            _marbleTabSettings = new TabItem();
            _marbleTabSettings.Header = "Marble";
            tabControl.Items.Add(_weightTabSettings);
            tabControl.Items.Add(_cogTabSettings);
            tabControl.Items.Add(_marbleTabSettings);
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
                NotifyMessage msg = new NotifyMessage(System.AppDomain.CurrentDomain.BaseDirectory + "images/GreenSkin.png", "Rapunzel", "Green Skin has been chosen.",
                                        () =>
                                        MessageBox.Show("Green Skin has been chosen.", "Green Skin", MessageBoxButton.OK));

                MainController.GetInstance.EnqueNotificationMessage(msg);
            }

            ti.Content = f;
        }

        public void NavigateSettings(TabItem ti)
        {
            Frame f = new Frame();

            if (ti.Equals(_weightTabSettings))
            {
                WeightSettings ws = new WeightSettings();
                f.Content = ws;
            }
            else if (ti.Equals(_cogTabSettings))
            {
                CogSettings cs = new CogSettings();
                f.Content = cs;
            }
            else if (ti.Equals(_marbleTabSettings))
            {
                
            }

            ti.Content = f;
        }
    }
}
