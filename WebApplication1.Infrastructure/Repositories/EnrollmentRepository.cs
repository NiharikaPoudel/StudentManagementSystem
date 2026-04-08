using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Domain.Models;
using WebApplication1.Infrastructure.Presistance;

namespace WebApplication1.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsAsync()
        {
            return await _context.Enrollment.ToListAsync();
        }

        public async Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment)
        {
            _context.Enrollment.Add(enrollment);
            await _context.SaveChangesAsync();
            return enrollment;
        }

        public async Task<bool> DeleteEnrollmentAsync(long studentId, long courseId)
        {
            var enrollment = await _context.Enrollment.FindAsync(studentId, courseId);
            if (enrollment == null) return false;

            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> BulkCreateEnrollmentsAsync(List<Enrollment> enrollments)
        {
            _context.Enrollment.AddRange(enrollments);
            await _context.SaveChangesAsync();
            return enrollments.Count;
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsFullDetailsAsync()
        {
            return await _context.Enrollment
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task<int> GetEnrollmentsCountAsync()
        {
            return await _context.Enrollment.CountAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByDateAsync(DateTime date)
        {
            return await _context.Enrollment
                .Where(e => e.EnrolledDate.Date == date.Date)
                .ToListAsync();
        }

        public async Task<bool> EnrollmentExistsAsync(long studentId, long courseId)
        {
            return await _context.Enrollment.AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }

        public async Task<bool> StudentExistsAsync(long studentId)
        {
            return await _context.Student.AnyAsync(s => s.Id == studentId);
        }

        public async Task<bool> CourseExistsAsync(long courseId)
        {
            return await _context.Course.AnyAsync(c => c.Id == courseId);
        }
    }
}
