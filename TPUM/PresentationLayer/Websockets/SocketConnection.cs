using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PresentationLayer.Websockets
{
    class SocketConnection
    {
        public Socket Socket { get; }
        public Func<string, Task> OnHandleResponse { get; }

        private IPEndPoint _endPoint;

        public SocketConnection(Socket socket, IPEndPoint remoteEndPoint, Func<string, Task> onHandleResponse)
        {
            Socket = socket;
            _endPoint = remoteEndPoint;
            Task.Factory.StartNew(() => MonitorConnection(socket));
            Console.WriteLine("Connected");
            OnHandleResponse = onHandleResponse;
        }

        private async Task MonitorConnection(Socket clientSocket)
        {
            byte[] bytes = new Byte[1024];
            string data = null;

            while (clientSocket.Connected)
            {
                int numByte = clientSocket.Receive(bytes);

                data += Encoding.ASCII.GetString(bytes, 0, numByte);

                if (data.IndexOf("<EOF>") > -1)
                {
                    data = data.Substring(0, data.Length - 5);
                    await OnHandleResponse(data);
                    data = null;
                }
                Trace.WriteLine($"RECEIVED:{data}");

            }
        }

        public async Task SendAsync(string message)
        {
            ArraySegment<byte> payload = new ArraySegment<byte>(Encoding.ASCII.GetBytes(message));
            await Socket.SendAsync(payload, SocketFlags.None);
        }
    }
}

