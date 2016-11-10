using SchoolRecords.Lib;
using SchoolRecords.Lib.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolRecords.Controllers
{
    [RoutePrefix("api/Library")]
    public class BookController : ApiController
    {
        private BookRepository LibraryBooks { get; }
        public BookController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            LibraryBooks = new BookRepository(connectionString);
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Book> GetBookList()
        {
            var bookList = LibraryBooks.Get();
            return bookList;
        }

        [HttpPost]
        [Route("{id}")]
        public Book GetBook(string id)
        {
            var oneBook = LibraryBooks.Get(id);
            return oneBook;
        }

        [HttpPost]
        [Route("")]
        public Book AddBook(Book book)
        {
            var addBook = LibraryBooks.Add(book);
            return addBook;
        }

        [HttpPut]
        [Route("{id}")]
        public Book UpdateBook(int id, Book book)
        {
            var updateBook = LibraryBooks.Update(id, book);
            return updateBook;
        }

        [HttpDelete]
        [Route("{id}")]
        public void RemoveBook(int id, Book book)
        {
            LibraryBooks.Remove(id);
        }
    }
     
}
