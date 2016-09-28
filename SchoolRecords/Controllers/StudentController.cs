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
        public List<Student> GetStudentList()
        {
            var allStudents = new List<Student>();

            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = "SELECT * FROM SchoolRecords..StudentTBL";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = (int)reader["StudentID"];
                    var firstName = reader["FirstName"].ToString();
                    var lastName = reader["LastName"].ToString();
                    var email = reader["Email"].ToString();
                    var student = new Student(id, firstName, lastName, email);
                    allStudents.Add(student);
                }
                connection.Close();
            }

            return allStudents;
        }

    }
}
