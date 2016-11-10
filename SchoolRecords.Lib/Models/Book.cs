using SchoolRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRecords.Lib.Models
{
    public class Book
    {
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public bool CheckedOut { get; set; }
        public DateTime ReturnDate { get; set; }
        public Student StudentId { get; set; }

        public Book(string bookName, string isbn, bool checkedOut)
        {
            BookName = bookName;
            ISBN = isbn;
            CheckedOut = checkedOut;
        }

        public Book(string bookName, string isbn, bool checkedOut, DateTime returnDate, Student student)
        {
            BookName = bookName;
            ISBN = isbn;
            CheckedOut = CheckedOut;
            ReturnDate = returnDate;
            StudentId = student;
        }
    }
}
