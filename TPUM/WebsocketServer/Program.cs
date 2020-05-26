using System;
using System.Net;
using System.Threading.Tasks;

namespace WebsocketServer
{

    class Program
    {
        static async Task Main(string[] args)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            Log(ipAddr.ToString());
            try
            {
                using WebsocketServer websocketServer = new WebsocketServer(Log, localEndPoint);
                await websocketServer.Listen();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Log("Unexpected error");
                Log(e.Message);
            }

        }

        private static readonly Action<string> Log = Console.WriteLine;

    }
}