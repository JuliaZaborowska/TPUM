using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebsocketServer.Resolver;

namespace WebsocketServer
{
    public class SocketConnection : IDisposable
    {
        public Socket Socket { get; }
        
        private IPEndPoint _endPoint;
        private Action<string> Log;
        private readonly RequestResolver _requestResolver;


        public SocketConnection(Socket socket, IPEndPoint remoteEndPoint, Action<string> log)
        {
            Log = s => Console.WriteLine(s);
            Socket = socket;
            _endPoint = remoteEndPoint;
            _requestResolver = new RequestResolver();
            Task.Factory.StartNew(() => MonitorConnection(socket));
        }

        public SocketConnection(Socket socket, IPEndPoint remoteEndPoint)
        {
            Log = (arg) => { };
            Socket = socket;
            _endPoint = remoteEndPoint;
            _requestResolver = new RequestResolver();
            Task.Factory.StartNew(() => MonitorConnection(socket));
        }

        private async Task MonitorConnection(Socket clientSocket)
        {
            Log("Connected");

            byte[] bytes = new Byte[1024];
            string data = null;

            while (true)
            {
                int numByte = await clientSocket.ReceiveAsync(bytes,SocketFlags.None);

                data += Encoding.ASCII.GetString(bytes, 0, numByte);

                if (data.IndexOf("<EOF>") > -1)
                {
                    data = data.Substring(0, data.Length - 5);
                    HandleRequest(data);
                    data = null;
                }
            }
        }

        public async void HandleRequest(string data)
        {
            Log("Request: ");
            Log(data);
            string response = _requestResolver.Resolve(data);
            Log("Response: ");
            Log(response);
            await SendAsync(response + "<EOF>");
        }

        public async Task SendAsync(string message)
        {
            ArraySegment<byte> payload = new ArraySegment<byte>(Encoding.ASCII.GetBytes(message));
            await Socket.SendAsync(payload, SocketFlags.None);
        }

        public void Dispose()
        {
            Socket?.Dispose();
        }
    }
}
