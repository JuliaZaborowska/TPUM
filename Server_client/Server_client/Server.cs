using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace Server_client
{
    class Server
    {
        private static TcpListener server;
        private static TcpClient client;
        public static void Main()
        {
            Server.EstablishClientConnection();
            NetworkStream stream = client.GetStream();

            while (true)
            {
                while (!stream.DataAvailable)
                {
                    Byte[] bytes = new Byte[client.Available];

                    stream.Read(bytes, 0, bytes.Length);
                }
            }
        }

        public static void EstablishClientConnection()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);
            server.Start();
            Console.WriteLine("Server has started on localhost 127.0.0.1:80. \nWaiting for connection...");
            client = server.AcceptTcpClient();
            Console.WriteLine("A client has connected.");
        }
    }
}
