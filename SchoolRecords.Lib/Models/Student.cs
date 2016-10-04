using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolRecords.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Student(int id, string firstName, string lastName, string email)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}