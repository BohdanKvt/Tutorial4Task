using System;
using Microsoft.AspNetCore.Mvc;
using Tutorial3.Models;
using System.Data.SqlClient;
using Tutorial3.Models.Enrollment;

namespace Tutorial3.Controllers
{
    [ApiController]
    [Route("api/enrollment")]
    public class EnrollmentController : Controller
    {
        [HttpPost]
        public IActionResult PostStudent(Enrollment1 student1)
        {
            string retunsemester = "2";
            if (String.IsNullOrWhiteSpace(student1.FirstName) || String.IsNullOrWhiteSpace(student1.LastName) || String.IsNullOrWhiteSpace(student1.IndexNumber) || String.IsNullOrWhiteSpace(student1.Studies) || String.IsNullOrWhiteSpace((student1.Birthdate).ToString()))
            {
                return BadRequest();
            }



            using (var sqlConnection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s16563;Integrated Security=True"))
            {
                sqlConnection.Open();
                var transaction = sqlConnection.BeginTransaction();
                string studyid = "0";


                using (var command = new SqlCommand())
                {

                    string temp = student1.Studies;

                    command.Connection = sqlConnection;
                    command.CommandText = "select IdStudy from Studies where Name = @temp;";
                    command.Parameters.AddWithValue("temp", temp);
                    command.Transaction = transaction;

                    var THERESPONSE = command.ExecuteReader();
                    if (!(THERESPONSE.Read()))
                    {
                        transaction.Rollback();
                        return BadRequest();

                    }
                    else
                    {
                        studyid = THERESPONSE["IdStudy"].ToString();

                    }
                    THERESPONSE.Close();


                    command.CommandText = "select IdEnrollment from Enrollment where Semester = 4 and IdStudy = @idstudy  Order By StartDate desc";
                    command.Parameters.AddWithValue("idstudy", studyid);

                    var THERESPONSE2 = command.ExecuteReader();
                    if (!(THERESPONSE2.Read()))
                    {

                        THERESPONSE2.Close();
                        DateTime now = System.DateTime.Now;
                        string sqlFormattedDate = now.ToString("yyyy-MM-dd");

                        command.CommandText = "insert into Enrollment(IdEnrollment, IdStudy, Semester, StartDate) values ( (select max(IdEnrollment)+1 from Enrollment) , @idd , 1, @sqldate);";
                        command.Parameters.AddWithValue("id2", studyid);
                        command.Parameters.AddWithValue("sqldate", sqlFormattedDate);


                        command.ExecuteNonQuery();

                    }
                    else
                    {
                        THERESPONSE2.Close();

                    }

                    string idEnroll = "0";
                    command.CommandText = "select  * from Enrollment where Semester = 3 and IdStudy = @id2study  Order By StartDate desc";
                    command.Parameters.AddWithValue("id2study", studyid);
                    
                }

                transaction.Commit();
                sqlConnection.Close();
            }



            var res = new Enrollment2();

            res.Studies = student1.Studies;
            res.Semester = Int32.Parse(retunsemester);

            return CreatedAtAction(nameof(PostStudent), res);
        }
       
        }

    }

