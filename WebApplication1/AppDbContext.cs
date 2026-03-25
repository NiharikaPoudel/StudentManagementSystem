using WebApplication1;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }

        public DbSet<Course> Course { get; set; }

        public DbSet<Module> Module { get; set; }

        public DbSet<ModuleInstructor> ModuleInstructor { get; set; }
        public DbSet<Instructor> Instructor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            modelBuilder.Entity<ModuleInstructor>()
                .HasKey(mi => new { mi.ModuleId, mi.InstructorId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
