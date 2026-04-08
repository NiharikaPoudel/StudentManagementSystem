using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Application.IServices;
using WebApplication1.Domain.Models;

namespace WebApplication1.Infrastructure.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<object>> GetAllCoursesWithModuleCountAsync()
        {
            return await _courseRepository.GetAllCoursesWithModuleCountAsync();
        }

        public async Task<Course> GetCourseWithModulesAsync(long id)
        {
            return await _courseRepository.GetCourseWithModulesAsync(id);
        }

        public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(long id)
        {
            return await _courseRepository.GetModulesByCourseIdAsync(id);
        }

        public async Task<IEnumerable<Student>> GetStudentsByCourseIdAsync(long id)
        {
            return await _courseRepository.GetStudentsByCourseIdAsync(id);
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            return await _courseRepository.CreateCourseAsync(course);
        }

        public async Task<Module> AddModuleToCourseAsync(long courseId, Module module)
        {
            return await _courseRepository.AddModuleToCourseAsync(courseId, module);
        }

        public async Task<bool> UpdateCourseAsync(long id, Course course)
        {
            if (id != course.Id)
            {
                return false;
            }

            if (!await _courseRepository.CourseExistsAsync(id))
            {
                return false;
            }

            return await _courseRepository.UpdateCourseAsync(id, course);
        }

        public async Task<bool> DeleteCourseAsync(long id)
        {
            return await _courseRepository.DeleteCourseAsync(id);
        }

        public async Task<int> BulkInsertCoursesAsync(List<Course> courses)
        {
            return await _courseRepository.BulkInsertCoursesAsync(courses);
        }

        public async Task<IEnumerable<Course>> GetCoursesWithDetailsAsync()
        {
            return await _courseRepository.GetCoursesWithDetailsAsync();
        }

        public async Task<int> GetCoursesCountAsync()
        {
            return await _courseRepository.GetCoursesCountAsync();
        }

        public async Task<int> GetTotalCreditsAsync()
        {
            return await _courseRepository.GetTotalCreditsAsync();
        }

        public async Task<IEnumerable<Course>> GetTopEnrolledCoursesAsync()
        {
            return await _courseRepository.GetTopEnrolledCoursesAsync();
        }
    }
}
