using System.ComponentModel.DataAnnotations;

public class Course
{
    [Key]
    public long Id { get; set; }

    public string Name { get; set; }

    public int DurationYears { get; set; }

    // Navigation
    public virtual ICollection<Module> Modules { get; set; }
    public virtual ICollection<Enrollment> Enrollments { get; set; }
}