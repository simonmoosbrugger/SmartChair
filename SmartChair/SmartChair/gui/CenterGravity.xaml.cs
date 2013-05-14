using SmartChair.controller;
using SmartChair.gui.controls;
using SmartChair.model;
using SmartChair.util;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SmartChair.gui
{
    /// <summary>
    /// Interaktionslogik für CenterGravity.xaml
    /// </summary>
    public partial class CenterGravity : Page, PageExtended, SmartChair.controller.DataController.SensorDataListener
    {
        private cog _cog;
        private List<KeyValuePair<DateTime, double>> source = new List<KeyValuePair<DateTime, double>>();

        public CenterGravity()
        {
            _cog = new cog(400, 400);
            InitializeComponent();
            //InitChart();
            MainController.GetInstance.DataController.AddSensorDataListener(this);
            coglive.Children.Add(_cog);
            lineChart.DataContext = source;

           
        }

        public void SensorDataUpdated(model.SensorData data)
        {
            //TODO: Save COG to db
            double x = data.Cog.X;
            double y = data.Cog.Y;
            source.Add(new KeyValuePair<DateTime, double>(data.Date, PointDistance.GetDistanceBetweenPoints(x, y)));

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

        void InitChart()
        {
            TestDataController controller = (TestDataController)MainController.GetInstance.DataController;
            int i = 0;
            DateTime time = DateTime.Now;

            foreach (SensorData data in controller.Data)
            {
                if (i == 100)
                {
                    break;
                }
                time = time.AddSeconds(5);
                source.Add(new KeyValuePair<DateTime, double>(time, PointDistance.GetDistanceBetweenPoints(data.Cog.X, data.Cog.Y)));
                i++;
            }
        }

        public bool RemoveListener()
        {
            return true;
        }
    }
}
