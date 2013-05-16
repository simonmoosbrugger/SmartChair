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
    /// Interaktionslogik für CogSettings.xaml
    /// </summary>
    public partial class CogSettings : Page
    {
        public CogSettings()
        {
            InitializeComponent();
            TimespanCogSave.Text = Properties.Settings.Default.TimespanCogSave.ToString();
        }

        private void TimespanCogSave_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Text != "")
            {
                Properties.Settings.Default.TimespanCogSave = int.Parse(box.Text);
            }
        }
    }
}
