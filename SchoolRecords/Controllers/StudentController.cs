using SchoolRecords.Lib;
using SchoolRecords.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolRecords.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentController : ApiController
    {
        private StudentRepository Students { get; set; }

        public StudentController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                
            Students = new StudentRepository(connectionString);
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Student> GetStudentList()
        {
            var studentList = Students.Get();
            return studentList;
        }

        [HttpPost]
        [Route("")]
        public Student AddStudent(Student student)
        {
            var newStudent = Students.Add(student);
            return newStudent;
        }

        [HttpPut]
        [Route("{id}")]
        public Student UpdateStudent(int id, Student student)
        {
            var newStudent = Students.Update(id, student);
            return newStudent;
        }

        [HttpDelete]
        [Route("{id}")]
        public void DeleteStudent(int id, Student student)
        {
            Students.Remove(id);
        }
    }
}
