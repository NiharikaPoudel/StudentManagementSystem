using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApplication1.Domain.Models
{
    public class Enrollment
    {
        [ForeignKey(nameof(Student))]
        public long StudentId { get; set; }

        [ForeignKey(nameof(Course))]
        public long CourseId { get; set; }

        public DateTime EnrolledDate { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
