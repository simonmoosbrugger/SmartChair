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
        public MainWindow()
        {
            InitializeComponent();
            MainController c = MainController.Controller;
            Weightcontrol wc = new Weightcontrol();
            c.DataController.AddListener(wc);
            MainFrame.Navigate(wc);
        }
    }
}
