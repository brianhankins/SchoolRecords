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
    public class CourseRepository
    {
        public CourseRepository()
        {

        }

        public IEnumerable<Course> Get()
        {
            var allcourses = new List<Course>();

            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = "SELECT Id, CourseName, CourseHours FROM SchoolRecords..CourseListTBL";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var name = reader["CourseName"].ToString();
                    var hours = reader["CourseHours"].ToString();
                    var course = new Course(id, name, hours);
                    allcourses.Add(course);
                }
                connection.Close();
            }
            return allcourses;
        }

        public IEnumerable<Course> Get(Course course)
        {

            var allCourses = new List<Course>();

            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = "SELECT Id, CourseName, CourseHours FROM SchoolRecords..CourseListTBL";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var name = reader["CourseName"].ToString();
                    var hours = reader["CourseHours"].ToString();
                    var singleCourse = new Course(id, name, hours);
                    allCourses.Add(singleCourse);
                }
                connection.Close();
            }
            return allCourses;
        }

        public Course Add(Course course)
        {
            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = @"INSERT INTO SchoolRecords..CourseListTBL(CourseName, CreditHours) 
                VALUES(@name, @hours)";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                command.Parameters.AddWithValue("name", course.Name);
                command.Parameters.AddWithValue("hours", course.Hours);

                command.ExecuteNonQuery();

                connection.Close();
            }
            return course;
        }

        public Course Update(int id, Course course)
        {
            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = @"UPDATE SchoolRecords..CourseListTBL 
                SET CourseName = @name, CourseHours = @hours
                WHERE ID = @id";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("name", course.Name);
                command.Parameters.AddWithValue("hours", course.Hours);

                command.ExecuteNonQuery();

                connection.Close();
            }
            return course;
        }


        public void Remove(int id)
        {
            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = @"DELETE SchoolRecords..CourseListTBL WHERE ID = @id";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                command.Parameters.AddWithValue("id", id);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}

