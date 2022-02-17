using BookApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Data
{
    public class SqlBookData : IBookData
    {
        private BookDbContext _bookContext;
        public SqlBookData(BookDbContext context)
        {
            _bookContext = context;
        } 
        public Book AddBook(Book book)
        {
            _bookContext.Books.Add(book);
            _bookContext.SaveChanges();
            return book;
        }

        public void DeleteBook(Book book)
        {
            _bookContext.Books.Remove(book);
            _bookContext.SaveChanges();
        }

        public Book EditBook(Book book)
        {
            var existingBook = GetBook(book.Id);
            existingBook.Id = book.Id;
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Description = book.Description;
            existingBook.Price = book.Price;
            _bookContext.SaveChanges();
            return existingBook;

        }

        public List<Book> GetAllBooks()
        {
          
          return  _bookContext.Books.ToList();

        }

        public Book GetBook(int id)
        {
            var book = _bookContext.Books.Find(id);
            return book;
            // return _bookContext.Books.SingleOrDefault(x => x.Id == id);
        }
    }
}
