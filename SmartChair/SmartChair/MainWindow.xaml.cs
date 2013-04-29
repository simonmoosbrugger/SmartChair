using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

using SmartChair.db;
using SmartChair.controller;
using SmartChair.gui;

namespace SmartChair
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainController _mc;

        public MainWindow()
        {
            InitializeComponent();

            //Init Maincontroller
            _mc = MainController.GetInstance;
            //_mc.NavigationController.setFrame(MainFrame);
            //_mc.NavigationController.navigateStart();

            GameController gc = new GameController(_mc.DataController);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //_mc.NavigationController.navigateCOG();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
           //_mc.NavigationController.navigateWeight();
        }

        GameController _gc;

        private void gs_start_Click(object sender, RoutedEventArgs e)
        {
            if (_gc == null)
            {
                _gc = new GameController(_mc.DataController);
                MessageBox.Show("Started");
            }
        }

        private void gs_stop_Click(object sender, RoutedEventArgs e)
        {
            if (_gc != null)
            {
                _gc.StopGc();
                MessageBox.Show("Closed");
            }
        }
    }
}
