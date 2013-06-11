using SmartChair.db;
using SmartChair.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WiimoteLib;

namespace SmartChair.controller
{
    public class WiiController : DataController
    {
        public WiiController()
        {
            Init();
        }

        private void Init()
        {
            try
            {
                Wiimote wm = new Wiimote();
                wm.WiimoteChanged += wm_WiimoteChanged;
                wm.Connect();
                wm.SetReportType(InputReport.IRAccel, true);
            }
            catch (Exception)
            {
                MessageBox.Show("Balance Board not connected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }


        }

        private BalanceBoardState _firstState;
        private bool _firstValue = true;
        private int BSL = 42, BSW = 49;

        private void wm_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            if (_firstValue == true && e.WiimoteState.BalanceBoardState.SensorValuesKg.TopLeft != 0 && e.WiimoteState.BalanceBoardState.SensorValuesKg.TopRight != 0 && e.WiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft != 0 && e.WiimoteState.BalanceBoardState.SensorValuesKg.BottomRight != 0)
            {
                _firstState = e.WiimoteState.BalanceBoardState;
                _firstValue = false;
            }

            float bl = e.WiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft - _firstState.SensorValuesKg.BottomLeft;
            float br = e.WiimoteState.BalanceBoardState.SensorValuesKg.BottomRight - _firstState.SensorValuesKg.BottomRight;
            float tl = e.WiimoteState.BalanceBoardState.SensorValuesKg.TopLeft - _firstState.SensorValuesKg.TopLeft;
            float tr = e.WiimoteState.BalanceBoardState.SensorValuesKg.TopRight - _firstState.SensorValuesKg.TopRight;
            //float wkg = e.WiimoteState.BalanceBoardState.WeightKg;

            float wkg = (tl + tr + bl + br) / 4.0f;

            float x = 0, y = 0;
            if (tr != 0 && br != 0 && bl != 0 && tl != 0)
            {
                float Kx = (tl + bl) / (tr + br);
                float Ky = (tl + tr) / (bl + br);

                x = ((float)(Kx - 1) / (float)(Kx + 1)) * (float)(-BSL / 2);
                y = ((float)(Ky - 1) / (float)(Ky + 1)) * (float)(-BSW / 2);
            }

            //float x = e.WiimoteState.BalanceBoardState.CenterOfGravity.X;
            //float y = e.WiimoteState.BalanceBoardState.CenterOfGravity.Y;

            SensorData data = new SensorData(bl, br, tl, tr, wkg, new SensorData.CenterOfGravity(x, y));
            SendSensorData(data);

            float battery = e.WiimoteState.Battery;
            SendBatteryStat(battery);
        }
    }
}
