using SmartChair.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace SmartChair.controller
{
    public class Point
    {
        private float _x, _y;

        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public Point(float x, float y)
        {
            _x = x;
            _y = y;
        }
    }

    public class MovementRecognitionController : SmartChair.controller.DataController.SensorDataListener
    {
        private DateTime _start;
        private int _minutes = 1;
        private int _minMovesCount = 20;

        private Point _initialPoint;
        private float _threshold = 2.0f;
        private int _pointsToAnalyse = 5;

        private bool _moved = false;
        private int _movesCount = 0;
        private int _count = 0,_countInArea = 0, _countOutArea = 0;

        public void SensorDataUpdated(SensorData data)
        {
            if (_initialPoint == null)
            {
                _initialPoint = new Point(data.Cog.X, data.Cog.Y);
                _start = DateTime.Now;
            }
            else if (_moved)
            {
                if (!inArea(data.Cog.X, data.Cog.Y))
                {
                    _countOutArea++;
                }
                else
                {
                    _countInArea++;
                }

                _count++;

                if (_count > _pointsToAnalyse)
                {
                    if (_countOutArea > _countInArea)
                    {
                        _movesCount++;
                        _initialPoint = new Point(data.Cog.X, data.Cog.Y);
                        _count = 0;
                        _countOutArea = 0;
                        _countInArea = 0;
                    }

                    _moved = false;
                }
            }
            else if (!inArea(data.Cog.X, data.Cog.Y))
            {
                _moved = true;
            }


            //TODO: 
            if ((DateTime.Now - _start).TotalMinutes > _minutes)
            {
                if (_movesCount < _minMovesCount)
                {
                    //MainController.GetInstance.NavigationController.Navigate(MainController.GetInstance.NavigationController._marbleTab)
                    MainController.GetInstance.EnqueNotificationMessage(new gui.controls.popup.NotifyMessage(System.AppDomain.CurrentDomain.BaseDirectory + "images/BlueSkin.png", "asdffsda", "Spiel???", () => MainController.GetInstance.NavigationController._marbleTab.IsSelected = true));
                    _start = DateTime.Now;
                    _movesCount = 0;
                }
            }
        }

        public bool inArea(float x, float y)
        {
            bool result = true;
            if (Math.Abs((_initialPoint.X - x)) > _threshold || Math.Abs((_initialPoint.Y - y)) > _threshold)
            {
                result = false;
            }
            return result;
        }
    }
}
