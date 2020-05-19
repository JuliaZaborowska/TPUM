using System;
using System.Net;
using System.Net.Sockets;

namespace LogicLayer
{
    public class WebSocketServer
    {
        Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        
        public void Start()
        {
            socketServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
            socketServer.Listen(256);
            socketServer.BeginAccept(null, 0, OnAccept, null);
        }            

        private void OnAccept(IAsyncResult result)
        {
            try
            {
                Socket client = null;
                if (socketServer != null && socketServer.IsBound)
                {
                    client = socketServer.EndAccept(result);
                }
                if (client != null)
                {
                    /* Handshaking and managing ClientSocket */
                }
            }
            catch (SocketException exception)
            {

            }
            finally
            {
                if (socketServer != null && socketServer.IsBound)
                {
                    socketServer.BeginAccept(null, 0, OnAccept, null);
                }
            }
        }
    }
}
