using Microsoft.EntityFrameworkCore;
using WebApplication1;
using CollegeManagementSystem;

namespace WebApplication1.Repositories.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Student.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(long id)
        {
            return await _context.Student.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetCoursesByStudentIdAsync(long id)
        {
            var student = await _context.Student
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
            
            return student?.Enrollments.Select(e => e.Course);
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            _context.Student.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
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

        public async Task<bool> DeleteStudentAsync(long id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return false;
            }
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> BulkInsertStudentsAsync(List<Student> students)
        {
            _context.Student.AddRange(students);
            await _context.SaveChangesAsync();
            return students.Count;
        }

        public async Task<IEnumerable<Student>> GetStudentsWithCoursesAsync()
        {
            return await _context.Student
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ToListAsync();
        }

        public async Task<int> GetStudentsCountAsync()
        {
            return await _context.Student.CountAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsFullDetailsAsync()
        {
            return await _context.Student
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                        .ThenInclude(c => c.Modules)
                .ToListAsync();
        }

        public async Task<bool> StudentExistsAsync(long id)
        {
            return await _context.Student.AnyAsync(e => e.Id == id);
        }
    }
}