using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.DataTransferObjects;
using Newtonsoft.Json;
using WebsocketServer.Resolver;

namespace WebsocketServer
{
    public class WebsocketServer : IDisposable
    {
        private readonly Action<string> Log;
        private readonly IPEndPoint _endpoint;
        private Socket _listener;
        public List<SocketConnection> Connections = new List<SocketConnection>();

        public WebsocketServer(Action<string> log, IPEndPoint endpoint)
        {
            Log = log;
            _endpoint = endpoint;
        }

        public async Task Listen()
        {
            _listener = new Socket(_endpoint.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                _listener.Bind(_endpoint);

                _listener.Listen(10);
                Log("Waiting for client to connect ... ");

                while (true)
                {
                    Socket clientSocket = _listener.Accept();
                    var connection = new SocketConnection(clientSocket, _endpoint, Log);
                    await InitializeConnection(connection);
                }
            }

            catch (Exception e)
            {
                Log(e.ToString());
            }
        }
        private async Task InitializeConnection(SocketConnection connection)
        {
            Connections.Add(connection);
            Log($"Maintaining {Connections.Count} active connections.");
            // await WriteAsync(connection.Socket, "Connected to server");
        }

        private async Task WriteAsync(Socket socket, String message)
        {
            Message msg = new Message() {Type="Text", Body = message};
            byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));
            var segments = new ArraySegment<byte>(buffer);
            await socket.SendAsync(segments, SocketFlags.None);
        }

        public void Dispose()
        {
            _listener?.Dispose();
        }
    }
}
