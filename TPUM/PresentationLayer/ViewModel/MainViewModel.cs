using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Windows.Input;
using LogicLayer.DataTransferObjects;
using Newtonsoft.Json;
using PresentationLayer.Commands;
using PresentationLayer.Websockets;

namespace PresentationLayer.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<UserDTO> _users;
        private ObservableCollection<BookDTO> _books;
        private ObservableCollection<DiscountCodeDTO> _discountCodes;
        private ObservableCollection<BookDTO> _cart = new ObservableCollection<BookDTO>(){new BookDTO(){Author="Test", Price = 123, Publisher = "O'Dupa"}};
        private DiscountCodeDTO _currentDiscountCode;
        private SocketConnection _connection;

        private WebsocketClient _websocketClient = new WebsocketClient("ws://localhost:9000/api");

        public ObservableCollection<UserDTO> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BookDTO> Books
        {
            get => _books;
            set
            {
                _books = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DiscountCodeDTO> DiscountCodes
        {
            get => _discountCodes;
            set
            {
                _discountCodes = value;
                OnPropertyChanged();
            }
        } 
        public ObservableCollection<BookDTO> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                OnPropertyChanged();
            }
        }

        public DiscountCodeDTO CurrentDiscountCode
        {
            get => _currentDiscountCode;
            set
            {
                _currentDiscountCode = value;
                OnPropertyChanged();
            }
        }

        public ICommand ConnectToWebsocketCommand => new RelayCommand(CreateConnection);
        public ICommand FetchUsersCommand => new RelayCommand(FetchUsers);
        public ICommand FetchBooksCommand => new RelayCommand(FetchBooks);
        public ICommand FetchDiscountCodesCommand => new RelayCommand(FetchDiscountCodes);

        private async void CreateConnection()
        {
            _connection = await _websocketClient.Connect(OnMessageReceived);
        }

        private async void FetchUsers()
        {
            if (_websocketClient.WebSocket.State == WebSocketState.Open)
            {
                Message messageSent = new Message() {Action = EndpointAction.GET_USERS.GetString()};
                await _connection.SendAsync(messageSent.ToString());
            }
        }
        private async void FetchBooks()
        {
            if (_websocketClient.WebSocket.State == WebSocketState.Open)
            {
                Message messageSent = new Message() { Action = EndpointAction.GET_BOOKS.GetString() };
                await _connection.SendAsync(messageSent.ToString());
            }
        }

        private async void FetchDiscountCodes()
        {
            if (_websocketClient.WebSocket.State == WebSocketState.Open)
            {
                Message messageSent = new Message() { Action = EndpointAction.GET_DISCOUNT_CODES.GetString() };
                await _connection.SendAsync(messageSent.ToString());
            }
        }

        private void OnMessageReceived(string data)
        {
            Trace.WriteLine("RECEIVED:");
            Trace.WriteLine(data);
            Message message = JsonConvert.DeserializeObject<Message>(data);
            Enum.TryParse(message.Action, out EndpointAction action);
            switch (action)
            {
                case EndpointAction.GET_BOOKS:
                    List<BookDTO> bookArray = JsonConvert.DeserializeObject<List<BookDTO>>(message.Body);
                    Books = new ObservableCollection<BookDTO>(bookArray);
                    break;
                case EndpointAction.GET_USERS:
                    List<UserDTO> userArray = JsonConvert.DeserializeObject<List<UserDTO>>(message.Body);
                    Users = new ObservableCollection<UserDTO>(userArray);
                    break;
                case EndpointAction.GET_DISCOUNT_CODES:
                    List<DiscountCodeDTO> discountArrays = JsonConvert.DeserializeObject<List<DiscountCodeDTO>>(message.Body);
                    DiscountCodes = new ObservableCollection<DiscountCodeDTO>(discountArrays);
                    break;
                case EndpointAction.PUBLISH_DISCOUNT_CODE:
                    DiscountCodeDTO code = JsonConvert.DeserializeObject<DiscountCodeDTO>(message.Body);
                    CurrentDiscountCode = code;
                    break;
            }
        }

        public MainViewModel()
        {
        }
    }
}