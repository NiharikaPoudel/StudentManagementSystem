
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Models;

namespace WebApplication1.Infrastructure.Presistance
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            modelBuilder.Entity<ModuleInstructor>()
                .HasKey(mi => new { mi.ModuleId, mi.InstructorId });

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "db57fad3-c45c-4cd5-a4ad-ca04b616b2a0" },
                new ApplicationRole { Id = 2, Name = "Instructor", NormalizedName = "INSTRUCTOR", ConcurrencyStamp = "db57fad3-c45c-4cd5-a4ad-ca04b616b2a1" },
                new ApplicationRole { Id = 3, Name = "Student", NormalizedName = "STUDENT", ConcurrencyStamp = "db57fad3-c45c-4cd5-a4ad-ca04b616b2a3" }
            );
        }
    }
}
