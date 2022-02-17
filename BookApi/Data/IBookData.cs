using BookApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Data
{
  public  interface IBookData
    {
        List<Book> GetAllBooks();

        Book GetBook(int id);

        Book AddBook(Book book);

        void DeleteBook(Book book); 

        Book EditBook(Book book);
    }
}
