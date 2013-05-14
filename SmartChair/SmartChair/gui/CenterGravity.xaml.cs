using SmartChair.controller;
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

        public CenterGravity()
        {
           
                InitializeComponent();
                InitChart();
                _centerPoint1CoordX = new Point(CenterPoint.X1, CenterPoint.X2);
                _centerPoint1CoordY = new Point(CenterPoint.Y1, CenterPoint.Y2);
                _centerPoint2CoordX = new Point(CenterPoint2.X1, CenterPoint2.X2);
                _centerPoint2CoordY = new Point(CenterPoint2.Y1, CenterPoint2.Y2);
                MainController.GetInstance.DataController.AddSensorDataListener(this);

           
           

        }

        public void SensorDataUpdated(model.SensorData data)
        {
            double x = data.Cog.X * _scale;
            double y = data.Cog.Y * _scale;

            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
                {
                    try
                    {
                        CenterPoint.X1 = _centerPoint1CoordX.X + x;
                        CenterPoint.X2 = _centerPoint1CoordX.Y + x;
                        CenterPoint.Y1 = _centerPoint1CoordY.X + y;
                        CenterPoint.Y2 = _centerPoint1CoordY.Y + y;
                        CenterPoint2.X1 = _centerPoint2CoordX.X + x;
                        CenterPoint2.X2 = _centerPoint2CoordX.Y + x;
                        CenterPoint2.Y1 = _centerPoint2CoordY.X + y;
                        CenterPoint2.Y2 = _centerPoint2CoordY.Y + y;
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
