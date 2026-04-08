using WebApplication1.Domain.Models;

namespace WebApplication1.Application.Interfaces.IRepositories
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetEnrollmentsAsync();
        Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment);
        Task<bool> DeleteEnrollmentAsync(long studentId, long courseId);
        Task<int> BulkCreateEnrollmentsAsync(List<Enrollment> enrollments);
        Task<IEnumerable<Enrollment>> GetEnrollmentsFullDetailsAsync();
        Task<int> GetEnrollmentsCountAsync();
        Task<IEnumerable<Enrollment>> GetEnrollmentsByDateAsync(DateTime date);
        Task<bool> EnrollmentExistsAsync(long studentId, long courseId);
        Task<bool> StudentExistsAsync(long studentId);
        Task<bool> CourseExistsAsync(long courseId);
    }
}
