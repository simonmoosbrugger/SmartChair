using SmartChair.db;
using SmartChair.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChair.controller
{
    public abstract class DataController
    {
        private List<SensorDataListener> _sensorDataListeners;
        private List<BatteryStatListener> _batteryStatListeners;
        protected DbController _dbController;

        public DataController()
        {
            _sensorDataListeners = new List<SensorDataListener>();
            _batteryStatListeners = new List<BatteryStatListener>();
        }

        public void AddListener(SensorDataListener listener)
        {
            _sensorDataListeners.Add(listener);
        }

        public void RemoveListener(SensorDataListener listener)
        {
            _sensorDataListeners.Remove(listener);
        }

        public void SendSensorData(SensorData data){
            foreach (SensorDataListener listener in _sensorDataListeners)
            {
                listener.SensorDataUpdated(data);
            }
        }

        public void SendBatteryStat(float value)
        {
            foreach (BatteryStatListener listener in _batteryStatListeners)
            {
                listener.BatteryStatUpdated(value);
            }
        }

        public interface SensorDataListener
        {
            void SensorDataUpdated(SensorData data);
        }

        public interface BatteryStatListener
        {
            void BatteryStatUpdated(float value);
        }
    }
}
