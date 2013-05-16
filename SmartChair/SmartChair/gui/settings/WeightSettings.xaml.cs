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

namespace SmartChair.gui
{
    /// <summary>
    /// Interaktionslogik für WeightSettings.xaml
    /// </summary>
    public partial class WeightSettings : Page
    {
        public WeightSettings()
        {
            InitializeComponent();
            TimespanWeightSave.Text = Properties.Settings.Default.TimespanWeightSave.ToString();
            
        }

        private void TimespanWeightSave_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Text != "")
            {
                Properties.Settings.Default.TimespanWeightSave = int.Parse(box.Text);
            }
        }
    }
}
