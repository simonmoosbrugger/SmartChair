using SmartChair.controller;
using SmartChair.db;
using SmartChair.gui.controls;
using SmartChair.model;
using SmartChair.util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using System.Windows.Threading;

namespace SmartChair.gui
{
    /// <summary>
    /// Interaktionslogik für CenterGravity.xaml
    /// </summary>
    public partial class CenterGravity : Page, PageExtended, SmartChair.controller.DataController.SensorDataListener
    {
        //TODO: Show when there is too less movement
        private cog _cog;
        private DateTime dtLast = new DateTime(), _dtStart;
        Series _serie;
        private bool _first;

        public CenterGravity()
        {
            InitializeComponent();

            Chart chart = new Chart();
            ChartArea area = new ChartArea();
            area.AxisX.Title = "t";
            area.AxisX.Minimum = 0;
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.Title = "Movement";
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            chart.ChartAreas.Add(area);
            _serie = new Series();
            _serie.ChartType = SeriesChartType.Line;
            _serie.MarkerStyle = MarkerStyle.Diamond;
            _serie.MarkerSize = 9;
            _serie.Color = Color.LimeGreen;
            chart.Series.Add(_serie);

            WindowsFormsHost host = new WindowsFormsHost();
            host.Child = chart;
            movlive.Children.Add(host);

            _cog = new cog(400, 400);
            coglive.Children.Add(_cog);

            _first = true;
            
            MainController.GetInstance.DataController.AddSensorDataListener(this);
        }

        object _lock = new object();

        public void SensorDataUpdated(model.SensorData data)
        {
            double x = data.Cog.X;
            double y = data.Cog.Y;

            if ((DateTime.Now - dtLast).TotalSeconds > Properties.Settings.Default.TimespanCogSave)
            {
                List<object> values = new List<object>();
                values.Add(DateTimeParser.getSQLiteSTringFromDateTime(DateTime.Now));
                values.Add(x);
                values.Add(y);
                values.Add(MainController.GetInstance.CurrentPerson.ID);
                MainController.GetInstance.DbController.Insert("CenterOfGravityData", DbUtil.GetColumnNames("CenterOfGravityData"), values);

                Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
        {
            try
            {
                if (_first)
                {
                    _dtStart = data.Date;
                    _first = false;
                }
                _serie.Points.Add(new DataPoint(Math.Round((data.Date - _dtStart).TotalSeconds, 0), PointDistance.GetDistanceBetweenPoints(x, y)));
            }
            catch (Exception)
            {

            }
        }));

                dtLast = DateTime.Now;

            }

            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            {
                try
                {
                    _cog.setPoint(x, y);
                }
                catch (Exception)
                {

                }
            }
            ));
        }

        public bool RemoveListener()
        {
            return true;
        }
    }
}
