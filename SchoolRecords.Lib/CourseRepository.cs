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
        private string Connection { get; }
        public CourseRepository(string connection)
        {
            Connection = connection;
        }

        public IEnumerable<Course> Get()
        {
            var allcourses = new List<Course>();

            var sql = @"SELECT T1.ID, T1.CourseName, T1.CreditHours, T1.InstructorID, T3.FirstName, T3.LastName, T3.Email, T3.RoomNumber
                        FROM CourseListTBL T1
                        JOIN InstructorTBL T3 ON T1.InstructorID = T3.ID";

            using (var connection = new SqlConnection(Connection))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var name = reader["CourseName"].ToString();
                    var hours = reader["CreditHours"].ToString();
                    var roomLocation = (int)reader["RoomNumber"];
                    var teacherId= (int)reader["InstructorID"];
                    var teacherFirstName = reader["FirstName"].ToString();
                    var teacherLastName = reader["LastName"].ToString();
                    var teacherEmail = reader["Email"].ToString();

                    Instructor teacher = new Instructor(teacherId, teacherFirstName, teacherLastName, teacherEmail);
                    var course = new Course(id, name, hours, roomLocation, teacher);

                    allcourses.Add(course);
                }
                connection.Close();
            }
            return allcourses;
        }

        public Course Get(string courseId)
        {
            var sql = @"SELECT T1.ID, T1.CourseName, T1.CreditHours, T1.InstructorID, T3.FirstName, T3.LastName, T3.Email, T3.RoomNumber
                        FROM CourseListTBL T1
                        JOIN InstructorTBL T3 ON T1.InstructorID = T3.ID
                        WHERE T1.ID = @courseId";

            using (var connection = new SqlConnection(Connection))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("courseId", courseId);

                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var name = reader["CourseName"].ToString();
                    var hours = reader["CreditHours"].ToString();
                    var roomLocation = (int)reader["RoomNumber"];
                    var teacherId = (int)reader["InstructorId"];
                    var teacherFirstName = reader["FirstName"].ToString();
                    var teacherLastName = reader["Lastname"].ToString();
                    var teacherEmail = reader["Email"].ToString();

                    Instructor teacher = new Instructor(teacherId, teacherFirstName, teacherLastName, teacherEmail);
                    var singleCourse = new Course(id, name, hours, roomLocation, teacher);
                    return singleCourse;
                }
                connection.Close();
            }
            throw new ArgumentException("Course not found");
        }

        public Course Add(Course course)
        {
            var sql = @"INSERT INTO SchoolRecords..CourseListTBL(CourseName, CreditHours, RoomNumber) 
                VALUES(@name, @hours, @room)";

            using (var connection = new SqlConnection(Connection))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                command.Parameters.AddWithValue("name", course.Name);
                command.Parameters.AddWithValue("hours", course.Hours);
                command.Parameters.AddWithValue("room", course.RoomNumber);

                command.ExecuteNonQuery();

                connection.Close();
            }
            return course;
        }

        public Course Update(int id, Course course)
        {
            var sql = @"UPDATE SchoolRecords..CourseListTBL 
                SET CourseName = @name, CourseHours = @hours, RoomNumber = @room
                WHERE ID = @id";

            using (var connection = new SqlConnection(Connection))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("name", course.Name);
                command.Parameters.AddWithValue("hours", course.Hours);
                command.Parameters.AddWithValue("room", course.RoomNumber);

                command.ExecuteNonQuery();

                connection.Close();
            }
            return course;
        }


        public void Remove(int id)
        {
            var sql = @"DELETE SchoolRecords..CourseListTBL WHERE ID = @id";

            using (var connection = new SqlConnection(Connection))
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

