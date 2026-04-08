using WebApplication1.Domain.Models;

namespace WebApplication1.Application.IServices
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIdAsync(long id);
        Task<IEnumerable<Course>> GetCoursesByStudentIdAsync(long id);
        Task<Student> CreateStudentAsync(Student student);
        Task<bool> UpdateStudentAsync(long id, Student student);
        Task<bool> DeleteStudentAsync(long id);
        Task<int> BulkInsertStudentsAsync(List<Student> students);
        Task<IEnumerable<Student>> GetStudentsWithCoursesAsync();
        Task<int> GetStudentsCountAsync();
        Task<IEnumerable<Student>> GetStudentsFullDetailsAsync();
    }
}
