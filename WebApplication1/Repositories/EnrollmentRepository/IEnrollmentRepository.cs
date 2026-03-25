namespace WebApplication1.Repositories.EnrollmentRepository
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetAllAsync();
        Task<Enrollment> GetByIdsAsync(long studentId, long courseId);
        Task<Enrollment> CreateAsync(Enrollment enrollment);
        Task<bool> UpdateAsync(Enrollment enrollment);
        Task<bool> DeleteAsync(long studentId, long courseId);
        Task<bool> ExistsAsync(long studentId, long courseId);
    }
}