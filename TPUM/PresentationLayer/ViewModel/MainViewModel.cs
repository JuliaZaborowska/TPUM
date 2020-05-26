using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using LogicLayer.DataTransferObjects;
using LogicLayer.Services.BookService;
using LogicLayer.Services.DiscountCodeService;
using LogicLayer.Services.HotShotPromotionService;
using LogicLayer.Services.UserService;
using Newtonsoft.Json;
using PresentationLayer.Commands;
using PresentationLayer.Model;
using PresentationLayer.Websockets;

namespace PresentationLayer.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
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

        private WebsocketClient _websocketClient = new WebsocketClient();
 

        private Uri _Uri;
        private string _UriPeer = "ws://localhost:8081/";

        // Example of proactive calls
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

        private void CreateConnection()
        {
            _websocketClient.InitializeClient(OnMessageReceived);
        }

        private async void FetchUsers()
        {
            Message messageSent = new Message() {Action = EndpointAction.GET_USERS};
            await _websocketClient.SendAsync(messageSent + "<EOF>");
        }       
        private async void FetchBooks()
        {
            Message messageSent = new Message() { Action = EndpointAction.GET_BOOKS };
            await _websocketClient.SendAsync(messageSent + "<EOF>");
        }        
        
        private async void FetchDiscountCodes()
        {
            Message messageSent = new Message() { Action = EndpointAction.GET_DISCOUNT_CODES };
            await _websocketClient.SendAsync(messageSent + "<EOF>");
        }

       private void OnMessageReceived(string data)
        {
            Trace.WriteLine("RECEIVED:");
            Trace.WriteLine(data);
            Message message = JsonConvert.DeserializeObject<Message>(data);
            if (message.Action == EndpointAction.GET_BOOKS)
            {
                var bookArray = JsonConvert.DeserializeObject<List<BookDTO>>(message.Body);
                var books = new ObservableCollection<BookDTO>(bookArray);
                Books = books;
            }
            else if (message.Action == EndpointAction.GET_USERS)
            {
                var userArray = JsonConvert.DeserializeObject<UserDTO[]>(message.Body);
                var users = new ObservableCollection<UserDTO>(userArray);
                Users = users;
            }
            else if (message.Action == EndpointAction.GET_DISCOUNT_CODES)
            {
                var discountArrays = JsonConvert.DeserializeObject<DiscountCodeDTO[]>(message.Body);
                var discountCodes = new ObservableCollection<DiscountCodeDTO>(discountArrays);
                DiscountCodes = discountCodes;
            }
        }
      
        public MainViewModel()
        {
            _publisher = new HotShotPromotionPublisher(TimeSpan.FromSeconds(5));
            _publisher.Start();
        }
    }
}