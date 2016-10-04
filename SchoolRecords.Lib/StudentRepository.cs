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
        public StudentRepository()
        {

        }

        public IEnumerable<Student> Get()
        {
            var allStudents = new List<Student>();

            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = "SELECT Id, FirstName, LastName, Email FROM SchoolRecords..StudentTBL";

            using (var connection = new SqlConnection(connectionString))
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

            var allStudents = new List<Student>();

            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = "SELECT Id, FirstName, LastName, Email FROM SchoolRecords..StudentTBL";

            using (var connection = new SqlConnection(connectionString))
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
                    var email = reader["email"].ToString();
                    var singleStudent = new Student(id, firstName, lastname, email);
                    allStudents.Add(singleStudent);
                }
                connection.Close();
            }
            return allStudents;
        }

        public Student Add(Student student)
        {
            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = @"INSERT INTO SchoolRecords..StudentTBL(FirstName, LastName, Email) 
                VALUES(@firstName, @lastName, @email)";

            using (var connection = new SqlConnection(connectionString))
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
            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = @"UPDATE SchoolRecords..StudentTBL 
                SET FirstName = @firstName, LastName = @lastName, Email = @email
                WHERE ID = @id";

            using (var connection = new SqlConnection(connectionString))
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
            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = @"DELETE SchoolRecords..InstructorTBL WHERE ID = @id";

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

