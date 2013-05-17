using MahApps.Metro.Controls;
using SmartChair.controller;
using SmartChair.gui.login;
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

namespace SmartChair.gui
{
    /// <summary>
    /// Interaktionslogik für LogIn.xaml
    /// </summary>
    public partial class LogIn : MetroWindow
    {
        public LogIn()
        {
            InitializeComponent();
            User.Content = MainController.GetInstance.PersonController.CurrentPerson.ToString();
        }

        private void SwitchUserButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchUser sw = new SwitchUser();
            sw.Show();
        }

        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            User.Content = MainController.GetInstance.PersonController.CurrentPerson.ToString();
        }
    }
}
