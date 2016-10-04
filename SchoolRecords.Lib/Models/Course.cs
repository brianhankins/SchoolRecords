using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolRecords.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Hours { get; set; }

        public Course(int id, string name, string hours)
        {
            ID = id;
            Name = name;
            Hours = hours;
        }
    }

    
}