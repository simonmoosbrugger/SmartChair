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
        private List<DataListener> _listeners;
        protected DbController _dbController;

        public DataController()
        {
            _listeners = new List<DataListener>();
        }

        public void AddListener(DataListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(DataListener listener)
        {
            _listeners.Remove(listener);
        }

        public void SendData(SensorData data){
            foreach (DataListener listener in _listeners)
            {
                listener.DataUpdated(data);
            }
        }

        public interface DataListener
        {
            void DataUpdated(SensorData data);
        }
    }
}
