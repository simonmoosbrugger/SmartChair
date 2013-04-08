using SmartChair.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameClientTest
{
    class Program
    {
        static TcpClient _clientSocket;

        static void Main(string[] args)
        {
            _clientSocket = new TcpClient();
            _clientSocket.Connect("127.0.0.1", 9900);

            Thread ctThread = new Thread(getMessage);
            ctThread.Start();
        }

        private static void getMessage()
        {
            while (true)
            {
                NetworkStream stream = _clientSocket.GetStream();
                BinaryFormatter formatter = new BinaryFormatter();
                object obj = formatter.Deserialize(stream);
                SensorData data = (SensorData)obj;
                Console.WriteLine(data.BottomLeft + " - " + data.BottomoRight + " - " + data.TopLeft + " - " + data.TopRight);
            }
        }
    }
}
