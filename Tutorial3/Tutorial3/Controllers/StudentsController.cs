using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Tutorial3.Models;

namespace Tutorial3.Controllers
{
    [ApiController]
    [Route("api/students")]

    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStudent()
        {
            var students = new List<Student>();
            using (var sqlConnection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s16563;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = sqlConnection;
                    command.CommandText = "select s.FirstName, s.LastName, s.BirthDate, st.Name as Studies, e.Semester" + " from Student s " + " join Enrollment e on e.IdEnrollment = s.IdEnrollment" + " join Studies st on st.IdStudy = e.IdStudy;";
                    sqlConnection.Open();
                    var response = command.ExecuteReader();
                    while (response.Read())
                    {
                        var st = new Student();
                        st.FirstName = response["FirstName"].ToString();
                        st.LastName = response["LastName"].ToString();
                        st.Studies = response["Studies"].ToString();
                        st.BirthDate = DateTime.Parse(response["BirthDate"].ToString());
                        st.Semester = int.Parse(response["Semester"].ToString());

                        students.Add(st);

                    }

                }
            }

            return Ok(students);
        }
   
        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
            string idx = "s123";
            idx = id;
            var students = new List<Student>();
            using (var sqlConnection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s16563;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = sqlConnection;
                    command.CommandText = "select s.FirstName, s.LastName, s.BirthDate, st.Name as Studies, e.Semester" + " from Student s " + " join Enrollment e on e.IdEnrollment = s.IdEnrollment" + " join Studies st on st.IdStudy = e.IdStudy" + " where s.IndexNumber =@idx;";
                    command.Parameters.AddWithValue("idx", idx);
                    sqlConnection.Open();
                    var response = command.ExecuteReader();
                    while (response.Read())
                    {
                        var st = new Student();
                        st.FirstName = response["FirstName"].ToString();
                        st.LastName = response["LastName"].ToString();
                        st.Studies = response["Studies"].ToString();
                        st.BirthDate = DateTime.Parse(response["BirthDate"].ToString());
                        st.Semester = int.Parse(response["Semester"].ToString());

                        students.Add(st);

                    }

                }

            }

            return Ok(students);
        }
    }
}
