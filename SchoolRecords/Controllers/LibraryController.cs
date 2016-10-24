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
    public class LibraryController : ApiController
    {
        private LibraryRepository LibraryBooks { get; }
        public LibraryController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            LibraryBooks = new LibraryRepository(connectionString);
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Library> GetBookList()
        {
            var bookList = LibraryBooks.Get();
            return bookList;
        }

        [HttpPost]
        [Route("{id}")]
        public Library GetBook(string id)
        {
            var oneBook = LibraryBooks.Get(id);
            return oneBook;
        }

        [HttpPost]
        [Route("")]
        public Library AddBook(Library book)
        {
            var addBook = LibraryBooks.Add(book);
            return addBook;
        }


    }
     
}
