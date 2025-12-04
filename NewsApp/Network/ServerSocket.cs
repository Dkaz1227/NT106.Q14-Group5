using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Network
{
    public class ServerSocket
    {
        private TcpListener listener;
        private bool isRunning = false;

        // Sự kiện dùng để đẩy log ra UI (WinForms)
        public event Action<string> OnLog;

        // Sự kiện dùng để gửi dữ liệu nhận được ra UI / Controller
        public event Action<string> OnMessageReceived;

        public void Start(int port = 5000)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                isRunning = true;

                Log($"Server started on port {port}");

                Thread acceptThread = new Thread(AcceptClients);
                acceptThread.IsBackground = true;
                acceptThread.Start();
            }
            catch (Exception ex)
            {
                Log("Start error: " + ex.Message);
            }
        }

        private void AcceptClients()
        {
            while (isRunning)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Log("Client connected");

                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
                catch (Exception ex)
                {
                    Log("Accept error: " + ex.Message);
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            try
            {
                byte[] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Log($"Received: {msg}");

                    // Đẩy message ra UI xử lý
                    OnMessageReceived?.Invoke(msg);

                    // >>> XỬ LÝ REQUEST TẠI ĐÂY <<<
                    // Sau đó gửi phản hồi cho client
                    string response = "OK: Server received your message";
                    SendMessage(stream, response);
                }
            }
            catch (Exception ex)
            {
                Log("Client error: " + ex.Message);
            }
            finally
            {
                client.Close();
                Log("Client disconnected");
            }
        }

        public void SendMessage(NetworkStream stream, string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch { }
        }

        public void Stop()
        {
            isRunning = false;
            listener?.Stop();
            Log("Server stopped");
        }

        private void Log(string msg)
        {
            OnLog?.Invoke(msg);
        }
    }
}
