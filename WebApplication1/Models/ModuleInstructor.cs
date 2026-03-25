using System.ComponentModel.DataAnnotations.Schema;

public class ModuleInstructor
{
    [ForeignKey(nameof(Module))]
    public long ModuleId { get; set; }

    [ForeignKey(nameof(Instructor))]
    public long InstructorId { get; set; }

    public virtual Module Module { get; set; }
    public virtual Instructor Instructor { get; set; }
}