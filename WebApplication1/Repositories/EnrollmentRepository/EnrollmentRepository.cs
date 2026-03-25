using Microsoft.EntityFrameworkCore;
using CollegeManagementSystem;

namespace WebApplication1.Repositories.EnrollmentRepository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollment.ToListAsync();
        }

        public async Task<Enrollment> GetByIdsAsync(long studentId, long courseId)
        {
            return await _context.Enrollment.FindAsync(studentId, courseId);
        }

        public async Task<Enrollment> CreateAsync(Enrollment enrollment)
        {
            _context.Enrollment.Add(enrollment);
            await _context.SaveChangesAsync();
            return enrollment;
        }

        public async Task<bool> UpdateAsync(Enrollment enrollment)
        {
            _context.Entry(enrollment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(long studentId, long courseId)
        {
            var enrollment = await _context.Enrollment.FindAsync(studentId, courseId);
            if (enrollment == null) return false;

            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(long studentId, long courseId)
        {
            return await _context.Enrollment.AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }
    }
}