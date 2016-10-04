using SchoolRecords.Lib;
using SchoolRecords.Models;
using System;
using System.Collections.Generic;
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
        [HttpGet]
        [Route("")]
        public IEnumerable<Student> GetStudentList()
        {
            var students = new StudentRepository();
            var studentList = students.Get();
            return studentList;
        }

        [HttpPost]
        [Route("")]
        public Student AddStudent(Student student)
        {
            var students = new StudentRepository();
            var newStudent = students.Add(student);
            return newStudent;
        }
        
        [HttpPut]
        [Route("{id}")]
        public Student UpdateStudent(int id, Student student)
        {
            var students = new StudentRepository();
            var newStudent = students.Update(id, student);
            return newStudent;
        } 

        [HttpDelete]
        [Route("{id}")]
        public void DeleteStudent(int id, Student student)
        {
            var students = new StudentRepository();
            students.Remove(id);
        }
    }
}
