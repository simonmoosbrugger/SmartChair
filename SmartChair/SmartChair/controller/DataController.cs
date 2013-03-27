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

        public DataController()
        {
            _listeners = new List<DataListener>();
        }

        public void sendData(SensorData data){
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
