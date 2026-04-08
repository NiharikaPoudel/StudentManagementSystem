using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApplication1.Domain.Models
{
    public class ModuleInstructor
    {
        [ForeignKey(nameof(Module))]
        public long ModuleId { get; set; }

        [ForeignKey(nameof(Instructor))]
        public long InstructorId { get; set; }

        public virtual Module Module { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}
