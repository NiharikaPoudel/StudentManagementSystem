using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Infrastructure.Presistance;

namespace WebApplication1.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetSummaryAsync()
        {
            var students = await _context.Student.CountAsync();
            var courses = await _context.Course.CountAsync();
            var modules = await _context.Module.CountAsync();
            var enrollments = await _context.Enrollment.CountAsync();

            return new
            {
                TotalStudents = students,
                TotalCourses = courses,
                TotalModules = modules,
                TotalEnrollments = enrollments
            };
        }
    }
}
