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
    [RoutePrefix("api/courses")]
    public class CourseController : ApiController
    {
        private CourseRepository Courses { get; }
        public CourseController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Courses = new CourseRepository(connectionString);
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Course> GetCourseList()
        {
            var courseList = Courses.Get();
            return courseList;
        }

        [HttpGet]
        [Route("{id}")]
        public Course GetCourse(string id)
        {
            var oneCourse = Courses.Get(id);
            return oneCourse;
        }

        [HttpPost]
        [Route("")]
        public Course AddCourse(Course course)
        {
            var addCourse = Courses.Add(course);
            return course;
        }
        
        [HttpPut]
        [Route("{id}")]
        public Course UpdateCourse(int id, Course course)
        {
            var updatedCourse = Courses.Update(id, course);
            return updatedCourse;
        } 

        [HttpDelete]
        [Route("{id}")]
        public void DeleteCourse(int id, Course course)
        {
            Courses.Remove(id);
        }
    }
}
