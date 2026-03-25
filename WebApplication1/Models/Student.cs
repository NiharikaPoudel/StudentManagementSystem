using System.ComponentModel.DataAnnotations;

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