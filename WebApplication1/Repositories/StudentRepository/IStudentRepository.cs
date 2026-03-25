namespace WebApplication1.Repositories.StudentRepository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIdAsync(long id);
        Task<IEnumerable<Course>> GetCoursesByStudentIdAsync(long id);
        Task<Student> CreateStudentAsync(Student student);
        Task<bool> UpdateStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(long id);
        Task<int> BulkInsertStudentsAsync(List<Student> students);
        Task<IEnumerable<Student>> GetStudentsWithCoursesAsync();
        Task<int> GetStudentsCountAsync();
        Task<IEnumerable<Student>> GetStudentsFullDetailsAsync();
        Task<bool> StudentExistsAsync(long id);
    }
}