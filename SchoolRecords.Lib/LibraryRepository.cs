﻿using SchoolRecords.Lib.Models;
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

        public Library Get(string studentId)
        {
            var sql = @"SELECT T1.ID, T1.BookName, T1.ISBN, T1.CheckedOut, T1.ReturnDate, T1. StudentID, 
                        T3.FirstName, T3.LastName, T3.Email
                      FROM LibraryTBL T1
                      JOIN StudentTBL T3 ON T1.StudentID = T3.ID
                      WHERE T1.ID = @studentId";
                      

            using (var connection = new SqlConnection(Connection))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("studentId", studentId);

                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = (int)reader["ID"];
                    var bookName = reader["BookName"].ToString();
                    var isbn = reader["ISBN"].ToString();
                    var checkedOut = (bool)reader["CheckedOut"];
                    var returnDate = (DateTime)reader["ReturnDate"];
                    var studentNumber = (int)reader["StudentID"];
                    var studentFirstName = reader["FirstName"].ToString();
                    var studentLastName = reader["LastName"].ToString();
                    var studentEmail = reader["Email"].ToString();

                    Student student = new Student(studentNumber, studentFirstName, studentLastName, studentEmail);
                    var libarybook = new Library(bookName, isbn, checkedOut, returnDate, student);

                    return libarybook;

                }
                connection.Close();
            }
            throw new ArgumentException("Student not found");
        }

        public Library Add(Library book)
        {
            var sql = @"INSERT INTO SchoolRecords..LibraryTBL(BookName, ISBN, CheckedOut, ReturnDate, StudentID)
                        VALUES(@bookName, @isbn, @checkedOut, returnDate, studentId)";

            using (var connection = new SqlConnection(Connection))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                command.Parameters.AddWithValue("bookName", book.BookName);
                command.Parameters.AddWithValue("isbn", book.ISBN);
                command.Parameters.AddWithValue("checkedOut", book.CheckedOut);
                command.Parameters.AddWithValue("returnDate", book.ReturnDate);
                command.Parameters.AddWithValue("studentId", book.StudentId);

                command.ExecuteNonQuery();

                connection.Close();
            }
            return book;
        }
        
        
    }
}
