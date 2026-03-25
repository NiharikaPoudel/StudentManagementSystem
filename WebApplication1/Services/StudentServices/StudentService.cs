using WebApplication1.Repositories.StudentRepository;

namespace WebApplication1.Services.StudentServices
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _studentRepository.GetStudentsAsync();
        }

        public async Task<Student> GetStudentByIdAsync(long id)
        {
            return await _studentRepository.GetStudentByIdAsync(id);
        }

        public async Task<IEnumerable<Course>> GetCoursesByStudentIdAsync(long id)
        {
            return await _studentRepository.GetCoursesByStudentIdAsync(id);
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            return await _studentRepository.CreateStudentAsync(student);
        }

        public async Task<bool> UpdateStudentAsync(long id, Student student)
        {
            if (id != student.Id)
            {
                return false;
            }

            if (!await _studentRepository.StudentExistsAsync(id))
            {
                return false;
            }

            return await _studentRepository.UpdateStudentAsync(student);
        }

        public async Task<bool> DeleteStudentAsync(long id)
        {
            return await _studentRepository.DeleteStudentAsync(id);
        }

        public async Task<int> BulkInsertStudentsAsync(List<Student> students)
        {
            return await _studentRepository.BulkInsertStudentsAsync(students);
        }

        public async Task<IEnumerable<Student>> GetStudentsWithCoursesAsync()
        {
            return await _studentRepository.GetStudentsWithCoursesAsync();
        }

        public async Task<int> GetStudentsCountAsync()
        {
            return await _studentRepository.GetStudentsCountAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsFullDetailsAsync()
        {
            return await _studentRepository.GetStudentsFullDetailsAsync();
        }
    }
}