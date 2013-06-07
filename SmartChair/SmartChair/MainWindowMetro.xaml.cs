using MahApps.Metro.Controls;
using SmartChair.controller;
using SmartChair.gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SmartChair.model;
using System.Diagnostics;
using System.Globalization;
using SmartChair.gui.settings;
using System.Threading;

namespace SmartChair
{
    

    /// <summary>
    /// Interaktionslogik für MainWindowMetro.xaml
    /// </summary>
    public partial class MainWindowMetro : MetroWindow
    {
        //TODO: Sensordaten sammeln für verschiedene Szenarien (arbeiten, lesen,...)

        MainController _mc;
        GameController _gc;

        public MainWindowMetro()
        {
            //CultureInfo culture = new CultureInfo("en-GB");

            //Thread.CurrentThread.CurrentCulture = culture;
            //Thread.CurrentThread.CurrentUICulture = culture;
            //FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(System.Windows.Markup.XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            InitializeComponent();
            

            _mc = MainController.GetInstance;
            _mc.NavigationController.InitTabs(tabControl);
            _gc = new GameController(_mc.DataController);
            Person p = _mc.CurrentPerson;
            if (_mc.PersonController.UserExists(Environment.UserName))
            {
                _mc.PersonController.CurrentPerson = _mc.PersonController.GetPersonByName(Environment.UserName);
            }
            else
            {
                _mc.PersonController.AddUser(_mc.PersonController.GetFirstname(Environment.UserName), _mc.PersonController.GetLastName(Environment.UserName));
                _mc.PersonController.CurrentPerson = _mc.PersonController.GetPersonByName(Environment.UserName);
            }
            loginName.DataContext = _mc.PersonController.CurrentPerson.ToString();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem ti = (TabItem)e.AddedItems[0];
            _mc.NavigationController.Navigate(ti);

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            if (settings.ShowDialog() == true)
            {

            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (Process p in Process.GetProcessesByName("marble"))
            {
                p.Kill();
            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            LogIn login = new LogIn();
            login.Show();
        }

        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            loginName.Text = MainController.GetInstance.PersonController.CurrentPerson.ToString();
        }
    }
}
