using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System;
using MS_Seed.Classes;
using MS_Seed;

namespace UtilCommon.Ethernet
{
    public class ControlTCP_IP
    {
        public FormMain FormMain { get; set; }

        private TcpListener Server;
        private List<TcpClient> ClientList = new List<TcpClient>();
        private bool IsListening = false;
        private readonly int MaxClients = int.Parse(ConfigurationManager.AppSettings["MAXCLIENT_TCP_IP"]);
        public event EventHandler<Tuple<Data, NetworkStream, IPEndPoint>> DataReceived;
        private Dictionary<TcpClient, IPEndPoint> clientInfo;
        public IPAddress ip = IPAddress.Parse(ConfigurationManager.AppSettings["IP_TCP_IP"]);
        public int port = int.Parse(ConfigurationManager.AppSettings["PORT_TCP_IP"]);

        public ControlTCP_IP()
        {
            Server = new TcpListener(ip, port);
            clientInfo = new Dictionary<TcpClient, IPEndPoint>();
        }

        public void Open()
        {
            try
            {
                Server.Start();
                IsListening = true;
                Files.WriteLog("Server started...");
                Task.Run(() => AcceptClients());
            }
            catch (Exception ex)
            {
                Files.WriteLog("Error starting server: " + ex.Message);
            }
        }

        private async Task AcceptClients()
        {
            try
            {
                while (IsListening && clientInfo.Count < MaxClients)
                {
                    TcpClient client = await Server.AcceptTcpClientAsync();
                    IPEndPoint clientEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                    NetworkStream stream = client.GetStream();

                    lock (clientInfo)
                    {
                        clientInfo[client] = clientEndPoint;
                        SendClient(stream, "OK\n");
                    }

                    Files.WriteLog("Client connected: " + clientEndPoint.ToString());

                    _ = Task.Run(() => HandleClient(client));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error accepting clients: " + ex.Message);
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Files.WriteLog("Data received from client: " + dataReceived);
                    IPEndPoint clientEndPoint = clientInfo[client];
                    ProcessData(dataReceived, stream, clientEndPoint);
                    // Gửi tín hiệu cho client
                    // SendResponseToClient(stream, response);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling client: " + ex.Message);
            }
            finally
            {
                client.Close();
                lock (ClientList)
                {
                    ClientList.Remove(client);
                }
            }
        }

        private void ProcessData(string input, NetworkStream stream, IPEndPoint clientEndPoint)
        {
            try
            {
                Data result = new Data();

                var data = input.Split(',');

                //result.WaitTime = data.Length > 0 ? data[0] : string.Empty;
                //result.Overtime = data.Length > 1 ? data[1] : string.Empty;
                //result.Pressure = data.Length > 2 ? data[2] : string.Empty;
                //result.TimeVacuum = data.Length > 3 ? data[3] : string.Empty;
                //result.TemperatureOrdinary = data.Length > 4 ? data[4] : string.Empty;
                //result.TimeStartLot = data.Length > 5 ? data[5] : string.Empty;
                //result.TimeEndLot = data.Length > 6 ? data[6] : string.Empty;
                //result.Vent = data.Length > 7 ? data[7] : string.Empty;
                //result.TimeVent = data.Length > 8 ? data[8] : string.Empty;
                //result.TemperatureWorking = data.Length > 9 ? data[9] : string.Empty;

                Console.WriteLine("test-data-receive");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SendClient(NetworkStream stream, string response)
        {
            try
            {
                byte[] responseBuffer = Encoding.ASCII.GetBytes(response);
                stream.Write(responseBuffer, 0, responseBuffer.Length);
                Files.WriteLog("Send data to client:" + response);

                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending response to client: " + ex.Message);
            }
        }

        public void Close()
        {
            try
            {
                IsListening = false;

                foreach (var client in ClientList)
                {
                    client.Close();
                }

                ClientList.Clear();

                Server.Stop();

                Files.WriteLog("Server stopped.");
            }
            catch (Exception ex)
            {
                Files.WriteLog("Error close server: " + ex.Message);
            }
        }
    }
}
