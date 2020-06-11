using System;
<<<<<<< HEAD
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using LogicLayer.DataTransferObjects;
using LogicLayer.Services.BookService;
using LogicLayer.Services.DiscountCodeService;
using LogicLayer.Services.HotShotPromotionService;
using LogicLayer.Services.UserService;
using PresentationLayer.Commands;
=======
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
>>>>>>> develop

namespace PresentationLayer.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
<<<<<<< HEAD
        private readonly IBookService _bookService = new BookService();
        private readonly IDiscountCodeService _discountService = new DiscountCodeService();
        private readonly IUserService _userService = new UserService();
        private ObservableCollection<UserDTO> _users;
        private ObservableCollection<BookDTO> _books;
        private ObservableCollection<DiscountCodeDTO> _discountCodes;
        private readonly HotShotPromotionPublisher _hotShotPublisher;
        private DiscountCodeDTO _currentDiscountCode;
        private HotShotPromotionPublisher _publisher;
        private IObservable<EventPattern<HotShotMessage>> _observable;
        private IDisposable _observer;

        //TODO:
        // private ObservableCollection<BookDTO> _cartBooks;
        // private CartDTO _cart;
        // private readonly ICartService _cartService = new CartService();


        // Example of proactive calls
        public ObservableCollection<UserDTO> Users
=======
        //private ObservableCollection<UserDTO> _users;
        private ObservableCollection<UserModel> _users;
        private ObservableCollection<BookModel> _books;
        private ObservableCollection<DiscountCodeModel> _discountCodes;
        private ObservableCollection<BookModel> _cart = new ObservableCollection<BookModel>(){new BookModel(){Author="Test", Price = 123, Publisher = "O'Duple"}};
        private DiscountCodeModel _currentDiscountCode;
        private SocketConnection _connection;

        private WebsocketClient _websocketClient = new WebsocketClient("ws://localhost:9000/api");

        public ObservableCollection<UserModel> Users
>>>>>>> develop
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

<<<<<<< HEAD
        public ObservableCollection<BookDTO> Books
=======
        public ObservableCollection<BookModel> Books
>>>>>>> develop
        {
            get => _books;
            set
            {
                _books = value;
                OnPropertyChanged();
            }
        }

<<<<<<< HEAD

        public ObservableCollection<DiscountCodeDTO> DiscountCodes
=======
        public ObservableCollection<DiscountCodeModel> DiscountCodes
>>>>>>> develop
        {
            get => _discountCodes;
            set
            {
                _discountCodes = value;
                OnPropertyChanged();
            }
<<<<<<< HEAD
        }

        public DiscountCodeDTO CurrentDiscountCode
        {
            get => _currentDiscountCode; 
=======
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
>>>>>>> develop
            set
            {
                _currentDiscountCode = value;
                OnPropertyChanged();
            }
        }

<<<<<<< HEAD
        public ICommand FetchUsersCommand  => new RelayCommand(FetchUsers);
        public ICommand FetchBooksCommand => new RelayCommand(FetchBooks);
        public ICommand FetchDiscountCodesCommand => new RelayCommand(FetchDiscountCodes);
        public ICommand SubscribeCommand => new RelayCommand(ConnectToHotShotService);

        public void ConnectToHotShotService()
        {
            _observable = Observable.FromEventPattern<HotShotMessage>(_publisher, "HotShotMessage");
            _observer = _observable.Subscribe(UpdateHotShotPromotion);
            //TODO: dispose if disconnecting
        }

        private void UpdateHotShotPromotion(EventPattern<HotShotMessage> args)
        {
            var discountCode = args.EventArgs.DiscountCode;
            CurrentDiscountCode = discountCode;
        }

        private void FetchUsers()
        {
            Users = new ObservableCollection<UserDTO>(_userService.GetAllUsers());
        }       
        private void FetchBooks()
        {
            Books = new ObservableCollection<BookDTO>(_bookService.GetAllBooks());
        }        
        
        private void FetchDiscountCodes()
        {
            DiscountCodes = new ObservableCollection<DiscountCodeDTO>(_discountService.GetAllDiscountCodes());
        }

        private void InitializeData()
        {
            // var currentUser = Users.First();
            // _cart = _cartService.GetCart(currentUser.Id.GetValueOrDefault());
            // _cartBooks = new ObservableCollection<BookDTO>(_cart.Books);
=======
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
>>>>>>> develop
        }

        public MainViewModel()
        {
<<<<<<< HEAD
            _publisher = new HotShotPromotionPublisher(TimeSpan.FromSeconds(5));
            _publisher.Start();
=======
>>>>>>> develop
        }
    }
}