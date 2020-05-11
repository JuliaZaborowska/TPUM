using System.Linq;
using DataLayer.Model;
using LogicLayer.DataTransferObjects;

namespace LogicLayer.ModelMapper
{
    public class DTOModelMapper
    {
        public Book FromBookDTO(BookDTO dto)
        {
            return new Book
            {
                Author = dto.Author,
                Id = dto?.Id,
                Price = dto.Price,
                Publisher = dto.Publisher,
                ReleaseYear = dto.ReleaseYear,
                Title = dto.Title
            };
        }

        public BookDTO ToBookDTO(Book book)
        {
            return new BookDTO
            {
                Author = book.Author,
                Id = book.Id,
                Price = book.Price,
                Publisher = book.Publisher,
                ReleaseYear = book.ReleaseYear,
                Title = book.Title
            };
        }

        public User FromUserDTO(UserDTO dto)
        {
            return new User
            {
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Phone = dto.Phone,
                Cart = dto.Cart,
                Email = dto.Email
            };
        }

        public UserDTO ToUserDTO(User user)
        {
            return new UserDTO
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                Phone = user.Phone,
                Cart = user.Cart,
                Email = user.Email
            };
        }

        public CartDTO ToCartDTO(Cart cart)
        {
            return new CartDTO
            {
                User = ToUserDTO(cart.User),
                Books = cart.Books.Select(ToBookDTO)
            };
        }

        public Cart FromCartDTO(CartDTO dto)
        {
            return new Cart
            {
                User = FromUserDTO(dto.User),
                Books = dto.Books.Select(FromBookDTO).ToList()
            };
        }

        public DiscountCodeDTO ToDiscountCodeDTO(DiscountCode discountCode)
        {
            return new DiscountCodeDTO
            {
                Code = discountCode.Code,
                Amount = discountCode.Amount
            };
        }

        public DiscountCode FromDiscountCodeDTO(DiscountCodeDTO dto)
        {
            return new DiscountCode
            {
                Code = dto.Code,
                Amount = dto.Amount
            };
        }
    }
}