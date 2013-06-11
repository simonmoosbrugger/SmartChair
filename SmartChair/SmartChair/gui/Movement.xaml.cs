using SmartChair.controller;
using SmartChair.model;
using SmartChair.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartChair.gui
{
    /// <summary>
    /// Interaktionslogik für Movement.xaml
    /// </summary>
    public partial class Movement : PageExtended
    {
        Series _serie;
        Chart _chart;


        public Movement()
        {
            InitializeComponent();

            _chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ChartArea area = new ChartArea();

            area.AxisX.Title = "t";
            area.AxisX.Minimum = 0;
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.Title = "Movement";
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


            dp1.SelectedDate = DateTime.Now.AddDays(-14);
            dp2.SelectedDate = DateTime.Now;

            dp1.SelectedDateChanged += dp1_SelectedDateChanged;
            dp2.SelectedDateChanged += dp2_SelectedDateChanged;

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

        private void dp1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            updateChart();
        }

        public void updateChart()
        {
            string date1 = dp1.SelectedDate.Value.ToString("MM.dd.yyyy") + " 00:00:00";
            string date2 = dp2.SelectedDate.Value.ToString("MM.dd.yyyy") + " 23:59:59";
            DataTable dt = MainController.GetInstance.DbController.Execute("SELECT * FROM CenterOfGravityData WHERE Timestamp >= '" + date1 + "' AND Timestamp < '" + date2 + "' AND PersonRef = " + MainController.GetInstance.CurrentPerson.ID + ";");

            List<KeyValuePair<DateTime, double>> values = new List<KeyValuePair<DateTime, double>>();
            foreach (DataRow row in dt.Rows)
            {
                DateTime date = DateTimeParser.getDateTimeFromSQLiteString(row["timestamp"].ToString());
                double x = (double)row["X"];
                double y = (double)row["Y"];
                _chart.Series[0].Points.AddXY(date, PointDistance.GetDistanceBetweenPoints(x, y));
                _chart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                _chart.ChartAreas[0].AxisX.LabelStyle.Format = "dd.MM.yyyy";
            }
        }

        public bool RemoveListener()
        {
            return true;
        }

       
    }
}
