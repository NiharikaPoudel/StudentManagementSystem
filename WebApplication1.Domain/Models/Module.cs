using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApplication1.Domain.Models
{
    public class Module
    {
        [Key]
        public long Id { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        [ForeignKey(nameof(Course))]
        public long CourseId { get; set; }

        public virtual Course Course { get; set; }


        public virtual ICollection<ModuleInstructor> ModuleInstructors { get; set; }
    }
}
