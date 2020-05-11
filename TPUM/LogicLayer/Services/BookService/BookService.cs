using System;
using System.Collections.Generic;
using DataLayer;
using DataLayer.Model;
using DataLayer.Repositories.Books;
using LogicLayer.DataTransferObjects;
using LogicLayer.ModelMapper;

namespace LogicLayer.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly DTOModelMapper _modelMapper;

        public BookService()
        {
            _bookRepository = new BookRepository(DataStore.Instance.State.Books);
            _modelMapper = new DTOModelMapper();
        }

        public BookService(BookRepository bookRepository, DTOModelMapper modelMapper)
        {
            _bookRepository = bookRepository;
            _modelMapper = modelMapper;
        }

        public BookDTO GetBookById(Guid id)
        {
            Book book = _bookRepository.Find(book => book.Id.Equals(id));
            return _modelMapper.ToBookDTO(book);
        }

        public IEnumerable<Book> GetAllBooks(Guid id)
        {
            return _bookRepository.Items;
        }

        public BookDTO AddBook(BookDTO dto)
        {
            Book book = _modelMapper.FromBookDTO(dto);
            Book created =  _bookRepository.Create(book);
            return _modelMapper.ToBookDTO(created);
        }

        public void DeleteBook(Guid book)
        {
            _bookRepository.Delete(book);
        }

        public BookDTO UpdateBook(BookDTO dto)
        {
            Book book = _modelMapper.FromBookDTO(dto);
            Book updated = _bookRepository.Update(book);
            return _modelMapper.ToBookDTO(updated);
        }

        public BookDTO Save(BookDTO bookDTO)
        {
            Book book = _modelMapper.FromBookDTO(bookDTO);
            Book updated = _bookRepository.CreateOrUpdate(book);
            return _modelMapper.ToBookDTO(updated);
        }

    }
}