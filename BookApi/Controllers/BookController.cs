using BookApi.Data;
using BookApi.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookData _repository;
        public BookController(IBookData repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAllBook()
        {
            return Ok(_repository.GetAllBooks());
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _repository.GetBook(id);

            if (book != null)
            {
                return Ok(book);
            }
            return NotFound($"Employee with Id: {id} was not found");
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            _repository.AddBook(book);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path +
                "/" + book.Id, book);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _repository.GetBook(id);

            if (book != null)
            {
                _repository.DeleteBook(book);
                return Ok();
            }
            return NotFound($"Employee with Id: {id} was not found");
        }

        [HttpPut("{id}")]
        public IActionResult EditBook(int id, Book book)
        {
            var existingBook = _repository.GetBook(id);

            if (existingBook != null)
            {
                book.Id = existingBook.Id;
                _repository.EditBook(book);
                return Ok();
            }
            return Ok(book);
        }
    }
} 
