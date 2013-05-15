using SmartChair.controller;
using SmartChair.gui.controls;
using SmartChair.model;
using SmartChair.util;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Threading;

namespace SmartChair.gui
{
    /// <summary>
    /// Interaktionslogik für CenterGravity.xaml
    /// </summary>
    public partial class CenterGravity : Page, PageExtended, SmartChair.controller.DataController.SensorDataListener
    {
        private cog _cog;
        private DateTime dtLast = new DateTime();

        public CenterGravity()
        {
            _cog = new cog(400, 400);
            InitializeComponent();
            source = new List<KeyValuePair<DateTime, double>>();
            MainController.GetInstance.DataController.AddSensorDataListener(this);
            coglive.Children.Add(_cog);
        }

        public void SensorDataUpdated(model.SensorData data)
        {
            double x = data.Cog.X;
            double y = data.Cog.Y;

            source.Add(new KeyValuePair<DateTime, double>(DateTime.Now, PointDistance.GetDistanceBetweenPoints(data.Cog.X, data.Cog.Y)));

            KeyValuePair<DateTime, double>[] temp = new KeyValuePair<DateTime, double>[source.Count];
            source.CopyTo(temp);

            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            {
                try
                {
                    if ((DateTime.Now - dtLast).TotalSeconds > 1)
                    {
                        LineSeries ls = new LineSeries()
                        {
                            DependentValuePath = "Value",
                            IndependentValuePath = "Key",
                            IsSelectionEnabled = true,
                            ItemsSource = temp,
                            //TODO color
                        };
                        lineChart.Series.Clear();
                        lineChart.Series.Add(ls);
                        dtLast = DateTime.Now;
                    }

                    _cog.setPoint(x, y);
                }
                catch (Exception)
                {

                }
            }
            ));
        }

        List<KeyValuePair<DateTime, double>> source;

        public bool RemoveListener()
        {
            return true;
        }
    }
}
