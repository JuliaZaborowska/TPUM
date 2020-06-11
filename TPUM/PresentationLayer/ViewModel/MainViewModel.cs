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
using PresentationLayer.Model;

namespace PresentationLayer.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        //private ObservableCollection<UserDTO> _users;
        private ObservableCollection<UserModel> _users;
        private ObservableCollection<BookModel> _books;
        private ObservableCollection<DiscountCodeModel> _discountCodes;
        private ObservableCollection<BookModel> _cart = new ObservableCollection<BookModel>(){new BookModel(){Author="Test", Price = 123, Publisher = "O'Duple"}};
        private DiscountCodeModel _currentDiscountCode;
        private SocketConnection _connection;

        private WebsocketClient _websocketClient = new WebsocketClient("ws://localhost:9000/api");

        public ObservableCollection<UserModel> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BookModel> Books
        {
            get => _books;
            set
            {
                _books = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DiscountCodeModel> DiscountCodes
        {
            get => _discountCodes;
            set
            {
                _discountCodes = value;
                OnPropertyChanged();
            }
        } 
        public ObservableCollection<BookModel> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                OnPropertyChanged();
            }
        }

        public DiscountCodeModel CurrentDiscountCode
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
                    List<BookModel> bookArray = JsonConvert.DeserializeObject<List<BookModel>>(message.Body);
                    Books = new ObservableCollection<BookModel>(bookArray);
                    break;
                case EndpointAction.GET_USERS:
                    List<UserModel> userArray = JsonConvert.DeserializeObject<List<UserModel>>(message.Body);
                    Users = new ObservableCollection<UserModel>(userArray);
                     break;
                case EndpointAction.GET_DISCOUNT_CODES:
                    List<DiscountCodeModel> discountArrays = JsonConvert.DeserializeObject<List<DiscountCodeModel>>(message.Body);
                    DiscountCodes = new ObservableCollection<DiscountCodeModel>(discountArrays);
                    break;
                case EndpointAction.PUBLISH_DISCOUNT_CODE:
                    DiscountCodeModel code = JsonConvert.DeserializeObject<DiscountCodeModel>(message.Body);
                    CurrentDiscountCode = code;
                    break;
            }
        }

        public MainViewModel()
        {
        }
    }
}