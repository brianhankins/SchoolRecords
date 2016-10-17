using SchoolRecords.Lib.Models;
using SchoolRecords.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRecords.Lib
{
    public class LibraryRepository
    {
        private string Connection { get; }
        public LibraryRepository(string connection)
        {
            Connection = connection;
        }

        public IEnumerable<Library> Get()
        {
            var allLibraryBooks = new List<Library>();

            var sql = "SELECT ID, BookName, ISBN, CheckedOut FROM SchoolRecords..LibraryTBL";

            using (var connection = new SqlConnection(Connection))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = (int)reader["ID"];
                    var bookName = reader["BookName"].ToString();
                    var isbn = reader["ISBN"].ToString();
                    var checkedOut = (bool)reader["CheckedOut"];

                    var libraryBook = new Library(bookName, isbn, checkedOut);

                    allLibraryBooks.Add(libraryBook);

                }
                connection.Close();
            }
            return allLibraryBooks;
        }

    }
}
