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
using System.Windows.Shapes;

namespace SmartChair
{
    /// <summary>
    /// Interaktionslogik für MainWindowMetro.xaml
    /// </summary>
    public partial class MainWindowMetro : MetroWindow
    {
        MainController _mc;
        GameController _gc;

        public MainWindowMetro()
        {
            InitializeComponent();
            _mc = MainController.GetInstance;
            _mc.NavigationController.InitTabs(tabControl);
            _gc = new GameController(_mc.DataController);
           
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem ti = (TabItem)e.AddedItems[0];
            _mc.NavigationController.Navigate(ti);
            
        }
    }
}
