using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;
using LogicLayer.Services.HotShotPromotionService;
using WebsocketServer;

namespace WebsocketServerLogic
{
    class WebsocketServer : IDisposable
    {
        WebsocketSerwerData.Data data;
        private HotShotPromotionPublisher _promotionPublisher;
        public List<WebSocketConnection> Connections = new List<WebSocketConnection>();

        public WebsocketServer(Action<string> log, string address)
        {
            data = new WebsocketSerwerData.Data(log, new HttpListener(), address);
            data.Listener.Prefixes.Add(address);
            _promotionPublisher = new HotShotPromotionPublisher(TimeSpan.FromSeconds(10));
            _promotionPublisher.Start();
        }

        public async Task Listen()
        {

            try
            {
                data.Listener.Start();

                data.Log($"Waiting for connections on {data.Address} ... ");


                while (true)
                {
                    HttpListenerContext httpListenerContext = await data.Listener.GetContextAsync();

                    if (httpListenerContext.Request.IsWebSocketRequest)
                    {
                        await InitializeConnection(httpListenerContext);
                    }

                }
            }

            catch (Exception e)
            {
                data.Log(e.ToString());
            }
        }

        private async Task InitializeConnection(HttpListenerContext context)
        {
            try
            {
                HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(subProtocol: null);
                WebSocketConnection connection = new WebSocketConnection(webSocketContext.WebSocket, data.Log);
                Connections.Add(connection);
                SubscribeToPromotions(connection);
                data.Log($"Maintaining {Connections.Count} active connections.");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.Close();
                data.Log($"Unable to establish connection: {ex.Message}.");
            }
        }

        private void SubscribeToPromotions(WebSocketConnection connection)
        {
            var observer = new PromotionObserver(connection);
            _promotionPublisher.Subscribe(observer);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
