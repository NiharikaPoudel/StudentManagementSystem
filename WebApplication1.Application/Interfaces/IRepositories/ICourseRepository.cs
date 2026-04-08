using WebApplication1.Domain.Models;

namespace WebApplication1.Application.Interfaces.IRepositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<object>> GetAllCoursesWithModuleCountAsync();
        Task<Course> GetCourseWithModulesAsync(long id);
        Task<IEnumerable<Module>> GetModulesByCourseIdAsync(long id);
        Task<IEnumerable<Student>> GetStudentsByCourseIdAsync(long id);
        Task<Course> CreateCourseAsync(Course course);
        Task<Module> AddModuleToCourseAsync(long courseId, Module module);
        Task<bool> UpdateCourseAsync(long id, Course course);
        Task<bool> DeleteCourseAsync(long id);
        Task<int> BulkInsertCoursesAsync(List<Course> courses);
        Task<IEnumerable<Course>> GetCoursesWithDetailsAsync();
        Task<int> GetCoursesCountAsync();
        Task<int> GetTotalCreditsAsync();
        Task<IEnumerable<Course>> GetTopEnrolledCoursesAsync();
        Task<bool> CourseExistsAsync(long id);
    }
}
