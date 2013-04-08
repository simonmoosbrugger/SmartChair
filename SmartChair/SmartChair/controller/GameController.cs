using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartChair.controller
{
    public class GameController : SmartChair.controller.DataController.SensorDataListener
    {
        private static object _lock;

        List<TcpClient> _clients;

        public void StopGc(){
            _controller.RemoveSensorDataListener(this);
            lock (_lock)
            {
                foreach (TcpClient client in _clients)
                {
                    client.Close();
                }
            }
            _serverSocket.Stop();
        }

        TcpListener _serverSocket;
        DataController _controller;

        public GameController(DataController controller)
        {
            _lock = new object();
            _clients = new List<TcpClient>();
            IPAddress address = IPAddress.Parse("127.0.0.1");
            _serverSocket = new TcpListener(address, 9900);
            
            _serverSocket.Start();
            Debug.WriteLine("Server Started");

            _controller = controller;
            _controller.AddSensorDataListener(this);

            Thread thread = new Thread(new ThreadStart(WaitForClients));
            thread.IsBackground = true;
            thread.Start();
        }

        private void WaitForClients()
        {
            while (true)
            {
                TcpClient clientSocket = _serverSocket.AcceptTcpClient();
                Debug.WriteLine("Client connected!");

                lock (_lock)
                {
                    _clients.Add(clientSocket);
                }
            }
        }

        public void SensorDataUpdated(model.SensorData data)
        {
            lock (_lock)
            {
                List<TcpClient> clientsToDelete = new List<TcpClient>();

                foreach (TcpClient client in _clients)
                {
                    try
                    {
                        NetworkStream stream = client.GetStream();
                        BinaryFormatter formatter = new BinaryFormatter();

                        formatter.Serialize(stream, data);
                    }
                    catch (Exception ex)
                    {
                        clientsToDelete.Add(client);
                    }
                }

                foreach (TcpClient c in clientsToDelete)
                {
                    _clients.Remove(c);
                }
            }
        }
    }
}