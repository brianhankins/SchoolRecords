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
    [RoutePrefix("api/instructors")]
    public class InstructorController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IEnumerable<Instructor> GetInstructorList()
        {
            var instructors = new InstructorRepository();
            var instructorList = instructors.Get();
            return instructorList;
        }

        [HttpPost]
        [Route("")]
        public Instructor AddInstructor(Instructor instructor)
        {
            var instructors = new InstructorRepository();
            var newInstructor = instructors.Add(instructor);
            return newInstructor;
        }

        [HttpPut]
        [Route("{id}")]
        public Instructor UpdateInstructor(int id, Instructor instructor)
        {
            var instructors = new InstructorRepository();
            var updatedInstructor = instructors.Update(id, instructor);
            return updatedInstructor;
        }

        [HttpDelete]
        [Route("{id}")]
        public void DeleteInstructor(int id, Instructor instructor)
        {
            var instructors = new InstructorRepository();
            instructors.Remove(id);
        }
    }

}
