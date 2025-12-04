using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Network
{
    public class ClientSocket
    {
        private TcpClient client;
        private NetworkStream stream;
        private bool isConnected = false;

        // Sự kiện để Form UI nhận log
        public event Action<string> OnLog;

        // Sự kiện để Form nhận message từ server
        public event Action<string> OnMessageReceived;

        // Kết nối đến server
        public bool Connect(string host = "127.0.0.1", int port = 5000)
        {
            try
            {
                client = new TcpClient();
                client.Connect(host, port);
                stream = client.GetStream();
                isConnected = true;

                Log($"Connected to server {host}:{port}");

                // Tạo thread lắng nghe dữ liệu từ server
                Thread listenThread = new Thread(ListenServer);
                listenThread.IsBackground = true;
                listenThread.Start();

                return true;
            }
            catch (Exception ex)
            {
                Log("Connect error: " + ex.Message);
                return false;
            }
        }

        private void ListenServer()
        {
            try
            {
                byte[] buffer = new byte[4096];

                while (isConnected)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead <= 0)
                        continue;

                    string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Log($"Received: {msg}");

                    // Đẩy message vào UI xử lý
                    OnMessageReceived?.Invoke(msg);
                }
            }
            catch (Exception ex)
            {
                Log("Listen error: " + ex.Message);
            }
        }

        // Gửi dữ liệu lên server
        public void Send(string message)
        {
            if (!isConnected)
            {
                Log("Not connected");
                return;
            }

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);

                Log("Sent: " + message);
            }
            catch (Exception ex)
            {
                Log("Send error: " + ex.Message);
            }
        }

        // Ngắt kết nối
        public void Disconnect()
        {
            try
            {
                isConnected = false;
                stream?.Close();
                client?.Close();
                Log("Disconnected");
            }
            catch { }
        }

        private void Log(string msg)
        {
            OnLog?.Invoke(msg);
        }
    }
}
