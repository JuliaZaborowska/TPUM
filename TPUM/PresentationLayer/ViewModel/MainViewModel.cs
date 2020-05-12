using System;
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

        //TODO:
        // private ObservableCollection<BookDTO> _cartBooks;
        // private CartDTO _cart;
        // private readonly ICartService _cartService = new CartService();


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
        }

        public MainViewModel()
        {
            _publisher = new HotShotPromotionPublisher(TimeSpan.FromSeconds(5));
            _publisher.Start();
        }
    }
}