using SmartChair.controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartChair.gui
{
    /// <summary>
    /// Interaktionslogik für StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void Cog_Click(object sender, RoutedEventArgs e)
        {
            MainController.GetInstance.NavigationController.navigateCOG();

        }

        private void weight_click(object sender, RoutedEventArgs e)
        {
            MainController.GetInstance.NavigationController.navigateWeight();
        }

        private void marble_click(object sender, RoutedEventArgs e)
        {
            FileInfo fi = new FileInfo(@"marble\marble.exe");
            Process.Start(fi.FullName);    
        }

        private void battery_click(object sender, RoutedEventArgs e)
        {
            MainController.GetInstance.NavigationController.navigateBattery();
        }

    }
}
