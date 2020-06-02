using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace PresentationLayer.Websockets
{
    class WebsocketClient: IDisposable
    {
        private readonly string _address;
        public ClientWebSocket WebSocket { get; private set; }    
        private SocketConnection _connection;

        public WebsocketClient(string address)
        {
            _address = address;
            WebSocket = new ClientWebSocket();
        }
        public async Task<SocketConnection> Connect(Action<string> handleResponse)
        {
            try
            {
                WebSocket = new ClientWebSocket();
                await WebSocket.ConnectAsync(new Uri(_address), CancellationToken.None);
                Trace.WriteLine($"Connecting to server ...");
                _connection = new SocketConnection(WebSocket, handleResponse);
                Trace.WriteLine($"Socket connected");
            }
            catch (ArgumentNullException ane)
            {

                Trace.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }

            catch (SocketException se)
            {

                Trace.WriteLine("SocketException : {0}", se.ToString());
            }

            catch (Exception e)
            {
                Trace.WriteLine($"Unexpected exception : {e}");
            }

            return _connection;
        }

        public void Dispose()
        {
            WebSocket?.Dispose();
            _connection?.Dispose();
        }
    }
}
