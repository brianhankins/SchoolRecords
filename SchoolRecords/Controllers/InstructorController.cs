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
    [RoutePrefix("api/instructors")]
    public class InstructorController : ApiController
    {
        private InstructorRepository Instructors { get; }
        public InstructorController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Instructors = new InstructorRepository(connectionString);
        }


        [HttpGet]
        [Route("")]
        public IEnumerable<Instructor> GetInstructorList()
        {
            var instructorList = Instructors.Get();
            return instructorList;
        }

        [HttpPost]
        [Route("")]
        public Instructor AddInstructor(Instructor instructor)
        {
            var newInstructor = Instructors.Add(instructor);
            return newInstructor;
        }

        [HttpPut]
        [Route("{id}")]
        public Instructor UpdateInstructor(int id, Instructor instructor)
        {
            var updatedInstructor = Instructors.Update(id, instructor);
            return updatedInstructor;
        }

        [HttpDelete]
        [Route("{id}")]
        public void DeleteInstructor(int id, Instructor instructor)
        {
            Instructors.Remove(id);
        }
    }

}
