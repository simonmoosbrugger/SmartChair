﻿using SmartChair.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiimoteLib;

namespace SmartChair.controller
{
    public class WiiController : DataController
    {
        private static WiiController _Instance;
      
        public static WiiController Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new WiiController();
                }
                return _Instance;
            }
        }

        private WiiController()
        {
            Init();
        }

        private void Init()
        {
            Wiimote wm = new Wiimote();
            wm.WiimoteChanged += wm_WiimoteChanged;
            wm.Connect();
            wm.SetReportType(InputReport.IRAccel, true);
        }

        private void wm_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            float bl = e.WiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft;
            float br = e.WiimoteState.BalanceBoardState.SensorValuesKg.BottomRight;
            float tl = e.WiimoteState.BalanceBoardState.SensorValuesKg.TopLeft;
            float tr = e.WiimoteState.BalanceBoardState.SensorValuesKg.TopRight;
            float wkg = e.WiimoteState.BalanceBoardState.WeightKg;
            float x = e.WiimoteState.BalanceBoardState.CenterOfGravity.X;
            float y = e.WiimoteState.BalanceBoardState.CenterOfGravity.Y;
            SensorData data = new SensorData(bl, br, tl, tr, wkg, new SensorData.CenterOfGravity(x, y));

            this.sendData(data);
        }
    }
}
