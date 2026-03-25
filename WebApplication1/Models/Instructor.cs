using System.ComponentModel.DataAnnotations;

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