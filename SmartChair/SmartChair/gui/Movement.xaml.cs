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
using System.Diagnostics;

namespace SmartChair.gui
{
    /// <summary>
    /// Interaktionslogik für Movement.xaml
    /// </summary>
    public partial class Movement : PageExtended
    {
        public Movement()
        {
            InitializeComponent();

            Area.AxisX.Title = "t";
            Area.AxisX.MajorGrid.LineColor = Color.LightGray;
            Area.AxisY.Title = "Movement";
            Area.AxisY.MajorGrid.LineColor = Color.LightGray;
            Serie.ChartType = SeriesChartType.Line;
            Serie.MarkerStyle = MarkerStyle.Diamond;
            Serie.MarkerSize = 9;
            Serie.Color = Color.LimeGreen;

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
            Serie.Points.Clear();
            string date1 = dp1.SelectedDate.Value.ToString("MM.dd.yyyy") + " 00:00:00";
            string date2 = dp2.SelectedDate.Value.ToString("MM.dd.yyyy") + " 23:59:59";
            Area.AxisX.LabelStyle.Format = ChartUtil.SelectLabelStyle(dp2.SelectedDate - dp1.SelectedDate);
            Area.AxisX.IntervalType = ChartUtil.SelectDateTimeInterval(dp2.SelectedDate - dp1.SelectedDate);
            DataTable dt = MainController.GetInstance.DbController.Execute("SELECT * FROM CenterOfGravityData WHERE Timestamp >= '" + date1 + "' AND Timestamp < '" + date2 + "' AND PersonRef = " + MainController.GetInstance.CurrentPerson.ID + ";");

            foreach (DataRow row in dt.Rows)
            {
                DateTime date = DateTimeParser.getDateTimeFromSQLiteString(row["timestamp"].ToString());
                double x = (double)row["X"];
                double y = (double)row["Y"];
                Serie.Points.AddXY(date, PointDistance.GetDistanceBetweenPoints(x, y));
            }
        }

        public bool RemoveListener()
        {
            return true;
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            switch (r.Next(0, 3))
            {
                case 0:
                    Area.AxisX.IntervalType = DateTimeIntervalType.Days;
                    Debug.WriteLine("Days");
                    Debug.WriteLine(dp2.SelectedDate - dp1.SelectedDate);
                    break;
                case 1:
                    Area.AxisX.IntervalType = DateTimeIntervalType.Hours;
                    Debug.WriteLine("Hours");
                    Debug.WriteLine(dp2.SelectedDate - dp1.SelectedDate);
                    break;
                case 2:
                    Area.AxisX.IntervalType = DateTimeIntervalType.Months;
                    Debug.WriteLine("Months");
                    Debug.WriteLine(dp2.SelectedDate - dp1.SelectedDate);
                    break;
                case 3:
                    Area.AxisX.IntervalType = DateTimeIntervalType.Minutes;
                    Debug.WriteLine("Minutes");
                    Debug.WriteLine(dp2.SelectedDate - dp1.SelectedDate);
                    break;
            }

        }
    }
}
