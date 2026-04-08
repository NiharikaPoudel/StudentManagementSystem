using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Domain.Models;
using WebApplication1.Infrastructure.Presistance;

namespace WebApplication1.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetAllCoursesWithModuleCountAsync()
        {
            return await _context.Course
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.DurationYears,
                    ModuleCount = c.Modules.Count
                })
                .ToListAsync();
        }

        public async Task<Course> GetCourseWithModulesAsync(long id)
        {
            return await _context.Course
                .Include(c => c.Modules)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(long id)
        {
            var course = await _context.Course
                .Include(c => c.Modules)
                .FirstOrDefaultAsync(c => c.Id == id);

            return course?.Modules;
        }

        public async Task<IEnumerable<Student>> GetStudentsByCourseIdAsync(long id)
        {
            var course = await _context.Course
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                .FirstOrDefaultAsync(c => c.Id == id);

            return course?.Enrollments.Select(e => e.Student);
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<Module> AddModuleToCourseAsync(long courseId, Module module)
        {
            var course = await _context.Course.FindAsync(courseId);
            if (course == null) return null;

            module.CourseId = courseId;
            _context.Module.Add(module);
            await _context.SaveChangesAsync();
            return module;
        }

        public async Task<bool> UpdateCourseAsync(long id, Course course)
        {
            var existingCourse = await _context.Course
                .Include(c => c.Modules)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCourse == null) return false;

            existingCourse.Name = course.Name;
            existingCourse.DurationYears = course.DurationYears;

            if (course.Modules != null)
            {
                var incomingModuleIds = course.Modules.Select(m => m.Id).Where(i => i != 0).ToList();
                var modulesToRemove = existingCourse.Modules.Where(m => !incomingModuleIds.Contains(m.Id)).ToList();

                foreach (var module in modulesToRemove)
                {
                    _context.Module.Remove(module);
                }

                foreach (var incomingModule in course.Modules)
                {
                    if (incomingModule.Id == 0)
                    {
                        incomingModule.CourseId = existingCourse.Id;
                        _context.Module.Add(incomingModule);
                    }
                    else
                    {
                        var existingModule = existingCourse.Modules.FirstOrDefault(m => m.Id == incomingModule.Id);
                        if (existingModule != null)
                        {
                            existingModule.Title = incomingModule.Title;
                            existingModule.Credits = incomingModule.Credits;
                        }
                    }
                }
            }

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

        public async Task<bool> DeleteCourseAsync(long id)
        {
            var course = await _context.Course
                .Include(c => c.Modules)
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return false;

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> BulkInsertCoursesAsync(List<Course> courses)
        {
            _context.Course.AddRange(courses);
            await _context.SaveChangesAsync();
            return courses.Count;
        }

        public async Task<IEnumerable<Course>> GetCoursesWithDetailsAsync()
        {
            return await _context.Course
                .Include(c => c.Modules)
                    .ThenInclude(m => m.ModuleInstructors)
                        .ThenInclude(mi => mi.Instructor)
                .ToListAsync();
        }

        public async Task<int> GetCoursesCountAsync()
        {
            return await _context.Course.CountAsync();
        }

        public async Task<int> GetTotalCreditsAsync()
        {
            return await _context.Module.SumAsync(m => m.Credits);
        }

        public async Task<IEnumerable<Course>> GetTopEnrolledCoursesAsync()
        {
            return await _context.Course
                .Include(c => c.Enrollments)
                .OrderByDescending(c => c.Enrollments.Count)
                .Take(5)
                .ToListAsync();
        }

        public async Task<bool> CourseExistsAsync(long id)
        {
            return await _context.Course.AnyAsync(e => e.Id == id);
        }
    }
}
