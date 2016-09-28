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
        public List<Instructor> GetInstructorList()
        {
            var allInstructors = new List<Instructor>();

            var connectionString = ("Server=localhost;Database=SchoolRecords;Trusted_Connection=True;");
            var sql = "SELECT * FROM SchoolRecords..InstructorTBL";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = (int)reader["InstructorID"];
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
    }
}
