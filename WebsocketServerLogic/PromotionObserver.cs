using System;
using DataLayer.Observer;
using LogicLayer.DataTransferObjects;
using LogicLayer.ModelMapper;
using Newtonsoft.Json;

namespace WebsocketServer
{
    public class PromotionObserver : IObserver<PromotionEvent>
    {
        private WebSocketConnection _connection;
        private DTOModelMapper mapper = new DTOModelMapper();
        public PromotionObserver(WebSocketConnection connection)
        {
            _connection = connection;
        }

        public void OnCompleted()
        {

        }

        public async void OnError(Exception error)
        {
            await _connection.SendAsync($"Error occured. Failed to fetch current promotion: {error.Message}");
        }

        public async void OnNext(PromotionEvent value)
        {
            Console.WriteLine("Cyclic message:", value);
            DiscountCodeDTO code = mapper.ToDiscountCodeDTO(value.DiscountCode);
            string body = JsonConvert.SerializeObject(code);
            Message message = new Message() { Action = EndpointAction.PUBLISH_DISCOUNT_CODE.GetString(), Type = "DiscountCodeDTO", Body = body };
            Console.WriteLine($"Promotion: {message}");
            await _connection.SendAsync(JsonConvert.SerializeObject(message));
        }
    }
}
