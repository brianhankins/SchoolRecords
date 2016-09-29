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
    public class InstructorRepository
    {
        public InstructorRepository()
        {

        }

        public IEnumerable<Instructor> Get()
        {
            var allInstructors = new List<Instructor>();

            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = "SELECT Id, FirstName, LastName, Email FROM SchoolRecords..InstructorTBL";

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
                    var instructor = new Instructor(id, firstName, lastname, email);
                    allInstructors.Add(instructor);
                }
                connection.Close();
            }
            return allInstructors;
        }

        public IEnumerable<Instructor> Get(Instructor instructor)
        {

            var allInstructors = new List<Instructor>();

            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = "SELECT Id, FirstName, LastName, Email FROM SchoolRecords..InstructorTBL";

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
                    var singleInstructor = new Instructor(id, firstName, lastname, email);
                    allInstructors.Add(singleInstructor);
                }
                connection.Close();
            }
            return allInstructors;
        }



        public Instructor Add(Instructor instructor)
        {
            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = @"INSERT INTO SchoolRecords..InstructorTBL(FirstName, LastName, Email) 
                VALUE(@firstName, @lastName, @email)";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                command.Parameters.AddWithValue("firstName", instructor.FirstName);
                command.Parameters.AddWithValue("lastName", instructor.LastName);
                command.Parameters.AddWithValue("email", instructor.Email);

                command.ExecuteNonQuery();

                connection.Close();
            }
            return instructor;
        }

        public Instructor Update(int id, Instructor instructor)
        {
            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = @"UPDATE SchoolRecords..InstructorTBL 
                SET FirstName = @firstName, LastName = @lastName, Email = @email
                WHERE ID = @id";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("firstName", instructor.FirstName);
                command.Parameters.AddWithValue("lastName", instructor.LastName);
                command.Parameters.AddWithValue("email", instructor.Email);

                command.ExecuteNonQuery();

                connection.Close();
            }
            return instructor;
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

