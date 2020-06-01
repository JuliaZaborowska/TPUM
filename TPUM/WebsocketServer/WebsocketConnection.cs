using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebsocketServer.Resolver;

namespace WebsocketServer
{
    public class WebSocketConnection : IDisposable
    {
        public WebSocket Socket { get; }
        private Action<string> Log;
        private readonly RequestResolver _requestResolver;


        public WebSocketConnection(WebSocket socket, Action<string> log)
        {
            Log = Console.WriteLine;
            Socket = socket;
            _requestResolver = new RequestResolver();
            Task.Factory.StartNew(() => MonitorConnection(socket));
        }

        private async Task MonitorConnection(WebSocket clientSocket)
        {
            Log("Connected");

            byte[] bytes = new byte[1024];

            while (true)
            {
                Array.Clear(bytes, 0, bytes.Length);
                WebSocketReceiveResult result = await clientSocket.ReceiveAsync(bytes, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client disconnected.", CancellationToken.None);
                }
                else
                {
                    var data = Encoding.UTF8.GetString(bytes).TrimEnd('\0');
                    data = data.Substring(0);
                    HandleRequest(data);
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
            await SendAsync(response.ToString());
        }

        public async Task SendAsync(string message)
        {
            ArraySegment<byte> payload = new ArraySegment<byte>(Encoding.ASCII.GetBytes(message));
            await Socket.SendAsync(payload, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public void Dispose()
        {
            Socket?.Dispose();
        }
    }
}
