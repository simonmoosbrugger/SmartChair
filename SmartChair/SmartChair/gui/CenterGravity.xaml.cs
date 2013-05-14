using SmartChair.controller;
using SmartChair.gui.controls;
using SmartChair.model;
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
        private Point _centerPoint1CoordX, _centerPoint1CoordY, _centerPoint2CoordX, _centerPoint2CoordY;
        private int _scale = 40;
        private cog _cog;

        public CenterGravity()
        {
            _cog = new cog(400, 400);
                InitializeComponent();
                InitChart();
                MainController.GetInstance.DataController.AddSensorDataListener(this);
                coglive.Children.Add(_cog);
        }

        public void SensorDataUpdated(model.SensorData data)
        {
            double x = data.Cog.X;
            double y = data.Cog.Y;

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
            List<KeyValuePair<DateTime, double>> source = new List<KeyValuePair<DateTime, double>>();
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
                source.Add(new KeyValuePair<DateTime, double>(time, GetDistanceBetweenPoints(data.Cog.X, data.Cog.Y)));
                i++;
            }
            lineChart.DataContext = source;
        }

        double GetDistanceBetweenPoints(double x, double y)
        {
            return Math.Sqrt(Math.Pow((0 - x), 2d) + Math.Pow((0 - y), 2d));
        }

        public bool RemoveListener()
        {
            return true;
        }
    }
}
