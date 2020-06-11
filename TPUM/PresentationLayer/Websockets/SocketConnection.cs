using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PresentationLayer.Websockets
{
    class SocketConnection : IDisposable
    {
        public WebSocket Socket { get; }
        public Action<string> OnHandleResponse { get; }

        public SocketConnection(WebSocket socket, Action<string> onHandleResponse)
        {
            Socket = socket;
            Task.Factory.StartNew(() => MonitorConnection(socket));
            OnHandleResponse = onHandleResponse;
        }

        private async Task MonitorConnection(WebSocket clientSocket)
        {
            byte[] bytes = new byte[4096];

            while (clientSocket.State == WebSocketState.Open)
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
                    OnHandleResponse(data);
                    Trace.WriteLine($"RECEIVED:{data}");
                }
            }
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

