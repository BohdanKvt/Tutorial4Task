using System;
using System.ComponentModel.DataAnnotations;


namespace Tutorial3.Models
{
    public class Enrollment1
    {

        public string IndexNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        [Required]
        public string Studies { get; set; }







    }
}