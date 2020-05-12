using System;
using System.Collections.Generic;
using DataLayer.Model;
using LogicLayer.DataTransferObjects;

namespace LogicLayer.Services.BookService
{
    public interface IBookService
    {
        BookDTO GetBookById(Guid id);
        IEnumerable<BookDTO> GetAllBooks();
        BookDTO AddBook(BookDTO book);
        void DeleteBook(Guid book);
        BookDTO UpdateBook(BookDTO book);
        BookDTO Save(BookDTO book);
    }
}