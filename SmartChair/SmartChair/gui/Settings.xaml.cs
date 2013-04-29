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

namespace SmartChair.gui
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        public Settings()
        {
            InitializeComponent();
            MainController.GetInstance.NavigationController.InitSettingsTabs(tabControl);

           
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem ti = (TabItem)e.AddedItems[0];
            MainController.GetInstance.NavigationController.NavigateSettings(ti);
        }
    }
}
