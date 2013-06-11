using SmartChair.controller;
using SmartChair.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;

namespace SmartChair.gui
{
    /// <summary>
    /// Interaktionslogik für weightcontrol.xaml
    /// </summary>
    public partial class Weightcontrol : Page, PageExtended, SmartChair.controller.DataController.SensorDataListener
    {
        DateTime timer = new DateTime();
        Series _serie;
        Chart _chart;


        public Weightcontrol()
        {
            InitializeComponent();

            _chart = new Chart();
            ChartArea area = new ChartArea();

            area.AxisX.Title = "t";
            area.AxisX.Minimum = 0;
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.Title = "KG";
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            _chart.ChartAreas.Add(area);
            _serie = new Series();
            _serie.ChartType = SeriesChartType.Line;
            _serie.MarkerStyle = MarkerStyle.Diamond;
            _serie.MarkerSize = 9;
            _serie.Color = Color.LimeGreen;
            _chart.Series.Add(_serie);

            WindowsFormsHost host = new WindowsFormsHost();
            host.Child = _chart;
            panel.Children.Add(host);


            MainController.GetInstance.DataController.AddSensorDataListener(this);



            dp1.SelectedDate = DateTime.Now.AddDays(-14);
            dp2.SelectedDate = DateTime.Now;

            dp1.DisplayDateEnd = DateTime.Now;

            dp1.SelectedDateChanged += dp1_SelectedDateChanged;
            dp2.SelectedDateChanged += dp2_SelectedDateChanged;
            updateChart();
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

            foreach (DataRow row in dt.Rows)
            {
                DateTime date = DateTimeParser.getDateTimeFromSQLiteString(row["timestamp"].ToString());
                double weight = (double)row["WeightKg"];
                _chart.Series[0].Points.AddXY(DateTime.Now, 65);
                //_chart.Series[0].Points.AddXY(date, weight);
                _chart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;

                //_chart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                //_chart.ChartAreas[0].AxisX.LabelStyle.Format = "dd.MM.yyyy";
            }
        }

        public bool RemoveListener()
        {
            return false;
        }

        public void SensorDataUpdated(model.SensorData data)
        {
            if (data.WeightKg > 10 && (DateTime.Now - timer).TotalMinutes >= Properties.Settings.Default.TimespanWeightSave)
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
