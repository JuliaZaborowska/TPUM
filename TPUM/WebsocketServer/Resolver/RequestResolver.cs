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
        private IUserService _userService;
        private IBookService _booksService;
        private IDiscountCodeService _discountCodeService;

        public RequestResolver()
        {
            _userService = new UserService();
            _booksService = new BookService();
            _discountCodeService = new DiscountCodeService();
        }

        public string Resolve(string message)
        {
            Message msgObj = JsonConvert.DeserializeObject<Message>(message);

            if (msgObj.Action == EndpointAction.GET_BOOKS)
            {
                IEnumerable<BookDTO> books = _booksService.GetAllBooks();
                Message response = new Message() { Action = EndpointAction.GET_BOOKS, Body = JsonConvert.SerializeObject(books), Type = "Array:Book" };
                return JsonConvert.SerializeObject(response);
            }

            if (msgObj.Action == EndpointAction.GET_USERS)
            {
                IEnumerable<UserDTO> users = _userService.GetAllUsers();
                Message response = new Message() {Action = EndpointAction.GET_USERS, Body = JsonConvert.SerializeObject(users), Type = "Array:User" };
                return JsonConvert.SerializeObject(response);
            }

            if (msgObj.Action == EndpointAction.GET_DISCOUNT_CODES)
            {
                IEnumerable<DiscountCodeDTO> discountCodes = _discountCodeService.GetAllDiscountCodes();
                Message response = new Message() {Action = EndpointAction.GET_DISCOUNT_CODES, Body = JsonConvert.SerializeObject(discountCodes), Type = "Array:DiscountCode" };
                return JsonConvert.SerializeObject(response);
            }

            throw new NotSupportedException();
        }
    }
}
