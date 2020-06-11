using System;
using System.Collections.Generic;
using LogicLayer.DataTransferObjects;
using LogicLayer.Services.BookService;
using LogicLayer.Services.DiscountCodeService;
using LogicLayer.Services.UserService;
using Newtonsoft.Json;

namespace WebsocketServer.Resolver
{

    class RequestResolver
    {
        private readonly IUserService _userService;
        private readonly IBookService _booksService;
        private readonly IDiscountCodeService _discountCodeService;

        public RequestResolver()
        {
            _userService = new UserService();
            _booksService = new BookService();
            _discountCodeService = new DiscountCodeService();
        }

        public string Resolve(string message)
        {
            Message msgObj = JsonConvert.DeserializeObject<Message>(message);
            EndpointAction action;
            Enum.TryParse(msgObj.Action, out action);
            Message response;

            switch (action)
            {
                case EndpointAction.GET_BOOKS:
                    IEnumerable<BookDTO> books = _booksService.GetAllBooks();
                    response = new Message() { Action = EndpointAction.GET_BOOKS.GetString(), Body = JsonConvert.SerializeObject(books), Type = "Array:Book" };
                    break;
          
                case EndpointAction.GET_USERS:
                    IEnumerable<UserDTO> users = _userService.GetAllUsers();
                     response = new Message() { Action = EndpointAction.GET_USERS.GetString(), Body = JsonConvert.SerializeObject(users), Type = "Array:User" };
                    break;

                case EndpointAction.GET_DISCOUNT_CODES:
                    IEnumerable<DiscountCodeDTO> discountCodes = _discountCodeService.GetAllDiscountCodes();
                     response = new Message() { Action = EndpointAction.GET_DISCOUNT_CODES.GetString(), Body = JsonConvert.SerializeObject(discountCodes), Type = "Array:DiscountCode" };
                    break;
                default:
                    throw new NotSupportedException("Requested action is not supported");
            }
            return JsonConvert.SerializeObject(response);

        }
    }
}
