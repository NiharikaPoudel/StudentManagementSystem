using Microsoft.EntityFrameworkCore;
using CollegeManagementSystem;

namespace WebApplication1.Repositories.CourseRepository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Course.ToListAsync();
        }

        public async Task<Course> GetByIdAsync(long id)
        {
            return await _context.Course.FindAsync(id);
        }

        public async Task<Course> CreateAsync(Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> UpdateAsync(Course course)
        {
            _context.Entry(course).State = EntityState.Modified;
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

        public async Task<bool> DeleteAsync(long id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null) return false;

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Course.AnyAsync(c => c.Id == id);
        }
    }
}
