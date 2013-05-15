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
        object _lock = new object();
        object _lock_bat = new object();

        public DataController()
        {
            _sensorDataListeners = new List<SensorDataListener>();
            _batteryStatListeners = new List<BatteryStatListener>();
        }

        public void AddSensorDataListener(SensorDataListener listener)
        {
            lock (_lock)
            {
                _sensorDataListeners.Add(listener);
            }
        }

        public void RemoveSensorDataListener(SensorDataListener listener)
        {
            lock (_lock)
            {
                _sensorDataListeners.Remove(listener);
            }
        }

        public void AddBatteryStatListener(BatteryStatListener listener)
        {
            lock (_lock_bat)
            {
                _batteryStatListeners.Add(listener);
            }
        }

        public void RemoveBatteryStatListener(BatteryStatListener listener)
        {
            lock (_lock_bat)
            {
                _batteryStatListeners.Remove(listener);
            }
        }

        public void SendSensorData(SensorData data)
        {
            lock (_lock)
            {
                foreach (SensorDataListener listener in _sensorDataListeners)
                {
                    listener.SensorDataUpdated(data);
                }
            }
        }

        public void SendBatteryStat(float value)
        {
            lock (_lock_bat)
            {
                foreach (BatteryStatListener listener in _batteryStatListeners)
                {
                    listener.BatteryStatUpdated(value);
                }
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
