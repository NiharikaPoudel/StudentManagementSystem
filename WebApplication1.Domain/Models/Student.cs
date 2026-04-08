using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApplication1.Domain.Models
{
    public class Student
    {

        [Key]
        public long Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        // Navigation
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
