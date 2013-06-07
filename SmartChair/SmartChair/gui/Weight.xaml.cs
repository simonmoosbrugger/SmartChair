using SmartChair.controller;
using SmartChair.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace SmartChair.gui
{
    /// <summary>
    /// Interaktionslogik für weightcontrol.xaml
    /// </summary>
    public partial class Weightcontrol : Page, PageExtended, SmartChair.controller.DataController.SensorDataListener
    {
        DateTime timer = new DateTime();

        public Weightcontrol()
        {
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

        private void dp2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dp1.SelectedDate.Value > dp2.SelectedDate.Value)
            {
                dp1.SelectedDate = dp2.SelectedDate.Value.AddDays(-14);
            }
            dp1.DisplayDateEnd = dp2.SelectedDate;
            updateChart();
        }

        public void updateChart()
        {
            string date1 = dp1.SelectedDate.Value.ToString("MM.dd.yyyy") + " 00:00:00";
            string date2 = dp2.SelectedDate.Value.ToString("MM.dd.yyyy") + " 23:59:59";
            DataTable dt = MainController.GetInstance.DbController.Execute("SELECT * FROM Weight WHERE Timestamp >= '" + date1 + "' AND Timestamp < '" + date2 + "' AND PersonRef = " + MainController.GetInstance.CurrentPerson.ID + ";");

            List<KeyValuePair<DateTime, double>> values = new List<KeyValuePair<DateTime, double>>();
            foreach (DataRow row in dt.Rows)
            {
                DateTime date = DateTimeParser.getDateTimeFromSQLiteString(row["timestamp"].ToString());
                double weight = (double)row["WeightKg"];
                values.Add(new KeyValuePair<DateTime, double>(date, weight));
            }
            lineChart.DataContext = values;
        }

        public bool RemoveListener()
        {
            return false;
        }

        public void SensorDataUpdated(model.SensorData data)
        {
            if ((DateTime.Now - timer).TotalMinutes >= Properties.Settings.Default.TimespanWeightSave)
            {
                List<object> values = new List<object>();
                values.Add(DateTimeParser.getSQLiteSTringFromDateTime(DateTime.Now));
                values.Add(data.WeightKg);
                values.Add(MainController.GetInstance.CurrentPerson.ID);
                MainController.GetInstance.DbController.Insert("Weight", DbUtil.GetColumnNames("Weight"), values);
                timer = DateTime.Now;
            }
        }
    }
}
