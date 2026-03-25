using System.ComponentModel.DataAnnotations.Schema;

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