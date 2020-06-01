using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using LogicLayer.DataTransferObjects;
using LogicLayer.Services.HotShotPromotionService;
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
        private readonly HotShotPromotionPublisher _hotShotPublisher;
        private DiscountCodeDTO _currentDiscountCode;
        private HotShotPromotionPublisher _publisher;
        private IObservable<EventPattern<HotShotMessage>> _observable;
        private IDisposable _observer;
        private SocketConnection _connection;

        private WebsocketClient _websocketClient = new WebsocketClient("ws://localhost:9000/api");

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
        public ICommand FetchUsersCommand => new RelayCommand(FetchUsers);
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

        private async void CreateConnection()
        {
            _connection = await _websocketClient.Connect(OnMessageReceived);
        }

        private async void FetchUsers()
        {
            Message messageSent = new Message() { Action = EndpointAction.GET_USERS.GetString() };
            await _connection.SendAsync(messageSent.ToString());
        }
        private async void FetchBooks()
        {
            Message messageSent = new Message() { Action = EndpointAction.GET_BOOKS.GetString() };
            await _connection.SendAsync(messageSent.ToString());
        }

        private async void FetchDiscountCodes()
        {
            Message messageSent = new Message() { Action = EndpointAction.GET_DISCOUNT_CODES.GetString() };
            await _connection.SendAsync(messageSent.ToString());
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
                    var bookArray = JsonConvert.DeserializeObject<List<BookDTO>>(message.Body);
                    var books = new ObservableCollection<BookDTO>(bookArray);
                    Books = books;
                    break;
                case EndpointAction.GET_USERS:
                    var userArray = JsonConvert.DeserializeObject<UserDTO[]>(message.Body);
                    var users = new ObservableCollection<UserDTO>(userArray);
                    Users = users;
                    break;
                case EndpointAction.GET_DISCOUNT_CODES:
                    var discountArrays = JsonConvert.DeserializeObject<DiscountCodeDTO[]>(message.Body);
                    var discountCodes = new ObservableCollection<DiscountCodeDTO>(discountArrays);
                    DiscountCodes = discountCodes;
                    break;
            }
        }

        public MainViewModel()
        {
            _publisher = new HotShotPromotionPublisher(TimeSpan.FromSeconds(5));
            _publisher.Start();
        }
    }
}