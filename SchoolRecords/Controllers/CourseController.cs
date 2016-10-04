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
    [RoutePrefix("api/courses")]
    public class CourseController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IEnumerable<Course> GetCourseList()
        {
            var course = new CourseRepository();
            var courseList = course.Get();
            return courseList;
        }

        [HttpPost]
        [Route("")]
        public Course AddCourse(Course course)
        {
            var courses = new CourseRepository();
            var addCourse = courses.Add(course);
            return course;
        }
        
        [HttpPut]
        [Route("{id}")]
        public Course UpdateCourse(int id, Course course)
        {
            var courses = new CourseRepository();
            var updatedCourse = courses.Update(id, course);
            return updatedCourse;
        } 

        [HttpDelete]
        [Route("{id}")]
        public void DeleteCourse(int id, Course course)
        {
            var courses = new CourseRepository();
            courses.Remove(id);
        }
    }
}
