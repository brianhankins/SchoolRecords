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
        private LibraryRepository LibraryBooks { get; }
        public BookController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            LibraryBooks = new LibraryRepository(connectionString);
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


    }
     
}
