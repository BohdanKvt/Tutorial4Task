
using Tutorial3.Models;
using System.Collections.Generic;


namespace Tutorial3
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}