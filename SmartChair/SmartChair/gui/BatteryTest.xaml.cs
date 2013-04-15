using SmartChair.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace SmartChair.gui
{
    /// <summary>
    /// Interaktionslogik für BatteryTest.xaml
    /// </summary>
    public partial class BatteryTest : Page, PageExtended, SmartChair.controller.DataController.BatteryStatListener 
    {
        public BatteryTest()
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
        }

        public bool RemoveListener()
        {
            return true;
        }

        public void BatteryStatUpdated(float value)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            {
                try
                {
                    progressBar1.Value = value;
                }
                catch (Exception)
                {

                }
            }
            ));
        }
    }
}
