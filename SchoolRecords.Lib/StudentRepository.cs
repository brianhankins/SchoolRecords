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

    public class StudentRepository
    {
        private string Connection { get; }
        public StudentRepository(string connection)
        {
            Connection = connection;
        }

        public IEnumerable<Student> Get()
        {
            var allStudents = new List<Student>();

            var sql = "SELECT Id, FirstName, LastName, Email FROM SchoolRecords..StudentTBL";

            using (var connection = new SqlConnection(Connection))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var firstName = reader["FirstName"].ToString();
                    var lastname = reader["LastName"].ToString();
                    var email = reader["Email"].ToString();
                    var student = new Student(id, firstName, lastname, email);
                    allStudents.Add(student);
                }
                connection.Close();
            }
            return allStudents;
        }

        public IEnumerable<Student> Get(Student student)
        {

            var singleStudents = new List<Student>();

            var sql = "SELECT Id, FirstName, LastName, Email FROM SchoolRecords..StudentTBL";

            using (var connection = new SqlConnection(Connection))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = (int)reader["Id"];
                        var firstName = reader["FirstName"].ToString();
                        var lastname = reader["LastName"].ToString();
                        var email = reader["email"].ToString();
                        var singleStudent = new Student(id, firstName, lastname, email);
                        singleStudents.Add(singleStudent);
                    }
                    connection.Close();
                }
                return singleStudents;
            }
        }

        public IEnumerable<Course> GetStudentCourses(Student student)
        {
            var studentCourses = new List<Course>();

            var sql = @"SELECT T2.ID, T2.CourseName, T2.CreditHours, T2.InstructorID, T3.LastName, T3.FirstName, T3.Email 
                        FROM StudentCourseXrefTBL T1
                        JOIN CourseListTBL T2 ON T1.CourseID = T2.ID
                        JOIN InstructorTBL T3 ON T2.InstructorID = T3.ID
                        WHERE T1.StudentID = @studentId";

            using (var connection = new SqlConnection(Connection))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                command.Parameters.AddWithValue("studentId", student.ID);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = (int)reader["ID"];
                    var instructorId = (int)reader["InstructorID"];
                    var firstName = reader["FirstName"].ToString();
                    var lastName = reader["LastName"].ToString();
                    var email = reader["Email"].ToString();
                    var courseName = reader["CourseName"].ToString();
                    var creditHours = reader["CreditHours"].ToString();

                    
                    var teacher = new Instructor(instructorId, firstName, lastName, email);
                    var course = new Course(id, courseName, creditHours, teacher);

                    studentCourses.Add(course);
                }
                connection.Close();
            }
            return studentCourses;
        }


        public Student Add(Student student)
        {
            var sql = @"INSERT INTO SchoolRecords..StudentTBL(FirstName, LastName, Email) 
                VALUES(@firstName, @lastName, @email)";

            using (var connection = new SqlConnection(Connection))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                command.Parameters.AddWithValue("firstName", student.FirstName);
                command.Parameters.AddWithValue("lastName", student.LastName);
                command.Parameters.AddWithValue("email", student.Email);

                command.ExecuteNonQuery();

                connection.Close();
            }
            return student;
        }

        public Student Update(int id, Student student)
        {
            var sql = @"UPDATE SchoolRecords..StudentTBL 
                SET FirstName = @firstName, LastName = @lastName, Email = @email
                WHERE ID = @id";

            using (var connection = new SqlConnection(Connection))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("firstName", student.FirstName);
                command.Parameters.AddWithValue("lastName", student.LastName);
                command.Parameters.AddWithValue("email", student.Email);

                command.ExecuteNonQuery();

                connection.Close();
            }
            return student;
        }

        public void Remove(int id)
        {
            var sql = @"DELETE SchoolRecords..InstructorTBL WHERE ID = @id";

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

