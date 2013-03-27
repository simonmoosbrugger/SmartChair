using SmartChair.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SmartChair.controller
{
    public class TestDataController : DataController
    {
        private DateTime? _lastBatteryDecrease;
        private float _batteryPercentage = 100;
        private List<SensorData> _data;
        private string _testData = @"controller\TestDataController_data";

        public TestDataController()
        {
            _data = new List<SensorData>();

            if (File.Exists(_testData + ".bin"))
            {
                DeserializeList();
            }
            else
            {
                string[] lines = File.ReadAllLines(_testData + ".txt");

                int i = 0;
                foreach (string line in lines)
                {
                    try
                    {
                        string[] values = line.Split(';');
                        float bl = float.Parse(values[0]);
                        float br = float.Parse(values[1]);
                        float tl = float.Parse(values[2]);
                        float tr = float.Parse(values[3]);
                        float x = float.Parse(Regex.Match(values[4], @"X=\-{0,1}(\d+\,\d+|\d+)", RegexOptions.Compiled).Groups[1].Value);
                        float y = float.Parse(Regex.Match(values[4], @"Y=\-{0,1}(\d+\,\d+|\d+)", RegexOptions.Compiled).Groups[1].Value);

                        Random rnd = new Random();
                        int w = rnd.Next(75, 86);
                        rnd = new Random();
                        int w2 = rnd.Next(1, 9);
                        float weight = float.Parse(w.ToString() + "," + w2.ToString());

                        _data.Add(new SensorData(bl, br, tl, tr, weight, new SensorData.CenterOfGravity(x, y)));
                        i++;
                    }
                    catch (Exception)
                    {
                    }
                }
                SerializeList();
            }

            Thread thread = new Thread(new ThreadStart(sendTestData));
            thread.Start();
        }

        private void SerializeList()
        {
            try
            {
                using (Stream stream = File.Open(_testData + ".bin", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, _data);
                }
            }
            catch (Exception)
            {
            }
        }

        private void DeserializeList()
        {
            try
            {
                using (Stream stream = File.Open(_testData + ".bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    _data = (List<SensorData>)bin.Deserialize(stream);
                }
            }
            catch (Exception)
            {
            }
        }

        private void sendTestData()
        {
            while (true)
            {
                foreach (SensorData data in _data)
                {
                    SendSensorData(data);
                    DateTime now = DateTime.Now;
                    if (_lastBatteryDecrease == null)
                    {
                        _lastBatteryDecrease = now;
                        SendBatteryStat(_batteryPercentage);
                    } else if (_batteryPercentage == 0 && (now - (DateTime)_lastBatteryDecrease).TotalMinutes >= 5)
                    {
                        _batteryPercentage = 100;
                    }else if ((now - (DateTime)_lastBatteryDecrease).TotalMinutes >= 1)
                    {
                        _lastBatteryDecrease = now;
                        _batteryPercentage -= 20;
                        SendBatteryStat(_batteryPercentage);
                    }

                    Thread.Sleep(2000);
                }
            }
        }
    }
}