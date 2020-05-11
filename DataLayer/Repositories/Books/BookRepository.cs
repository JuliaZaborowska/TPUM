using System.Collections.Generic;
using DataLayer.Model;

namespace DataLayer.Repositories.Books
{
    public class BookRepository : CrudRepository<Book>, IBookRepository
    {
        public BookRepository(IList<Book> books) : base(books)
        {
        }
    }
}