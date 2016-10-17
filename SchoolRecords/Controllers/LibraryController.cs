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
        private LibraryRepository Library { get; }
        public LibraryController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Library = new LibraryRepository(connectionString);
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Library> GetBookList()
        {
            var bookList = Library.Get();
            return bookList; 
        }

    }
     
}
