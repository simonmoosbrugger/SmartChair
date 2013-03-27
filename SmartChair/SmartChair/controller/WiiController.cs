using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiimoteLib;

namespace SmartChair.controller
{
    class WiiController
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
            //e.WiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft
        }
    }
}
