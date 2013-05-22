using MahApps.Metro.Controls;
using SmartChair.controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartChair.gui.login
{
    /// <summary>
    /// Interaktionslogik für SwitchUser.xaml
    /// </summary>
    public partial class SwitchUser : MetroWindow
    {
        public SwitchUser()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (!MainController.GetInstance.PersonController.UserExists(FirstName.Text, LastName.Text))
            {
                MainController.GetInstance.PersonController.AddUser(FirstName.Text, LastName.Text);
                MainController.GetInstance.PersonController.CurrentPerson = MainController.GetInstance.PersonController.GetPersonByName(FirstName.Text, LastName.Text);
                this.Close();
            }
            else
            {
                ErrorMsg.Content = "User already exists";
            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (MainController.GetInstance.PersonController.UserExists(FirstName.Text, LastName.Text))
            {
                MainController.GetInstance.PersonController.CurrentPerson = MainController.GetInstance.PersonController.GetPersonByName(FirstName.Text, LastName.Text);
                this.Close();
            }
            else
            {
                ErrorMsg.Content = "User does not exist";
            }
        }
    }
}
