using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApplication1.Domain.Models
{
    public class Instructor
    {
        [Key]
        public long Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime HireDate { get; set; }

        // Many-to-many via ModuleInstructor
        public virtual ICollection<ModuleInstructor> ModuleInstructors { get; set; }
    }
}
