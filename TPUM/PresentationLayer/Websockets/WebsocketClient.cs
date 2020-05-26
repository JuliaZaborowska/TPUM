using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PresentationLayer.Websockets
{
    class WebsocketClient
    {
        public WebsocketClient()
        {

        }

        public Socket Sender { get; set; }
        public void InitializeClient(Action<string> handleResponse)
        {

            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

                Sender = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);
                Task.Factory.StartNew(() => MonitorConnection(Sender, handleResponse));

                try
                {
                    Sender.Connect(localEndPoint);
                    Trace.WriteLine($"Socket connected to -> {Sender.RemoteEndPoint}");
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
            }

            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }

        public async Task SendAsync(string message)
        {
            Trace.WriteLine("SENDING MESSAGE");
            Trace.WriteLine(message);

            ArraySegment<byte> payload = new ArraySegment<byte>(Encoding.ASCII.GetBytes(message));
            await Sender.SendAsync(payload, SocketFlags.None);
        }

        public async Task<Message> ReceiveAsync()
        {
            byte[] messageReceived = new byte[1024];
            int byteRecv = await Sender.ReceiveAsync(messageReceived, SocketFlags.None);
            string message = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
            return JsonConvert.DeserializeObject<Message>(message);
        }

        private async Task MonitorConnection(Socket clientSocket, Action<string> onHandleResponse)
        {
            Trace.WriteLine("Connected");

            byte[] bytes = new Byte[1024];
            string data = null;

            while (clientSocket.Connected)
            {
                int numByte = await clientSocket.ReceiveAsync(bytes, SocketFlags.None);

                data += Encoding.ASCII.GetString(bytes, 0, numByte);

                if (data.IndexOf("<EOF>") > -1)
                {
                    data = data.Substring(0, data.Length - 5);
                    onHandleResponse(data);
                    data = null;
                }
            }
            Trace.WriteLine("Request: ");
            Trace.WriteLine(data);
        }
    }
}
