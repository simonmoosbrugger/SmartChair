using SmartChair.controller;
using SmartChair.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    /// Interaktionslogik für weightcontrol.xaml
    /// </summary>
    public partial class Weightcontrol : Page
    {
        private MainController _c;

        public Weightcontrol()
        {
            _c = MainController.Controller;
            InitializeComponent();
            
            dp1.SelectedDate = DateTime.Now.AddDays(-14);
            dp2.SelectedDate = DateTime.Now;

            dp1.DisplayDateEnd = DateTime.Now;

            dp1.SelectedDateChanged += dp1_SelectedDateChanged;
            dp2.SelectedDateChanged += dp2_SelectedDateChanged;

            //showChart();
            updateChart();
        }

        private void showChart()
        {
            List<KeyValuePair<string, int>> values = new List<KeyValuePair<string, int>>();
            values.Add(new KeyValuePair<string, int>("21.03.2013", 78));
            values.Add(new KeyValuePair<string, int>("22.03.2013", 80));
            values.Add(new KeyValuePair<string, int>("23.03.2013", 81));
            values.Add(new KeyValuePair<string, int>("24.03.2013", 82));
            values.Add(new KeyValuePair<string, int>("25.03.2013", 81));
            values.Add(new KeyValuePair<string, int>("26.03.2013", 80));
            lineChart.DataContext = values;
        }

        private void dp1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            updateChart();
        }

        //DateTime.Now.ToString("yyyy-MM-dd HH:MM")

        private void dp2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dp1.SelectedDate.Value > dp2.SelectedDate.Value)
            {
                dp1.SelectedDate = dp2.SelectedDate.Value.AddDays(-14);
            }
            dp1.DisplayDateEnd = dp2.SelectedDate;
            updateChart();
        }

        private void updateChart()
        {
            
            string date1 = dp1.SelectedDate.Value.ToString("MM.dd.yyyy") + " 00:00:00";
            string date2 = dp2.SelectedDate.Value.ToString("MM.dd.yyyy") + " 23:59:59";
            DataTable dt = MainController.Controller.DbController.Execute("SELECT * FROM Weight WHERE Timestamp >= '" + date1 + "' AND Timestamp < '" + date2 + "' AND PersonRef = " + MainController.Controller.PersonController.CurrentPerson.ID + ";");

            List<KeyValuePair<DateTime, long>> values = new List<KeyValuePair<DateTime, long>>();
            foreach (DataRow row in dt.Rows)
            {
                DateTime date = DateTimeParser.getDateTimeFromSQLiteString(row["timestamp"].ToString());
                long weight = (long)row["WeightKg"];
                values.Add(new KeyValuePair<DateTime, long>(date, weight));
            }
            lineChart.DataContext = values;
        }
    }
}
