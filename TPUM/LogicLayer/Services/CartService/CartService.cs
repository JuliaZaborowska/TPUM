using System;
using System.Linq;
using DataLayer;
using DataLayer.Model;
using DataLayer.Repositories.Books;
using DataLayer.Repositories.DiscountCodeRepository;
using DataLayer.Repositories.Users;
using LogicLayer.DataTransferObjects;
using LogicLayer.ModelMapper;

namespace LogicLayer.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IDiscountCodeRepository _dicountCodeRepository;
        private readonly DTOModelMapper _modelMapper;

        public CartService()
        {
            _userRepository = new UsersRepository(DataStore.Instance.State.Users);
            _bookRepository = new BookRepository(DataStore.Instance.State.Books);
            _dicountCodeRepository = new DiscountCodeRepository(DataStore.Instance.State.DiscountCodes);
            _modelMapper = new DTOModelMapper();
        }

        public CartService(IBookRepository bookRepository, IUserRepository userRepository, IDiscountCodeRepository discountCodeRepository, DTOModelMapper modelMapper)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _dicountCodeRepository = discountCodeRepository;
            _modelMapper = modelMapper;
        }

        public CartDTO AddBookToCart(Guid bookId, Guid userId)
        {
            User user = _userRepository.Find(u => u.Id.Equals(userId));
            Book book = _bookRepository.Find(b => b.Id.Equals(bookId));
            user.Cart.Books.Add(book);
            return _modelMapper.ToCartDTO(user.Cart);
        }

        public CartDTO RemoveBookFromCart(Guid bookId, Guid userId)
        {
            User user = _userRepository.Find(u => u.Id.Equals(userId));
            Book book = _bookRepository.Find(b => b.Id.Equals(bookId));
            user.Cart.Books.Remove(book);
            return _modelMapper.ToCartDTO(user.Cart);
        }

        public decimal CalculateTotalPrice(Guid userId, string code)
        {
            User user = _userRepository.Find(u => u.Id.Equals(userId));
            decimal rawPrice = user.Cart.Books.Sum(book => book.Price);
            if (code != null)
            {
                DiscountCode discountCode = _dicountCodeRepository.Find(dc => dc.Code.Equals(code));
                if (discountCode != null)
                {
                    return rawPrice * (100 - discountCode.Amount) / 100;
                }
            }

            return rawPrice;

        }
    }
}