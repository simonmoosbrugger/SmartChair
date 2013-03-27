using SmartChair.db;
using SmartChair.model;
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
        public WiiController(DbController dbController)
        {
            Init();
            _dbController = dbController;
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
            SendSensorData(data);

            float battery = e.WiimoteState.Battery;
            SendBatteryStat(battery);
        }
    }
}
