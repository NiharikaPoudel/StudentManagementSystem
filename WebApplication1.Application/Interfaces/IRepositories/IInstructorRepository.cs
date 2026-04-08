using WebApplication1.Domain.Models;

namespace WebApplication1.Application.Interfaces.IRepositories
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetInstructorsAsync();
        Task<Instructor> GetInstructorByIdAsync(long id);
        Task<Instructor> CreateInstructorAsync(Instructor instructor);
        Task<bool> UpdateInstructorAsync(Instructor instructor);
        Task<bool> DeleteInstructorAsync(long id);
        Task<int> BulkInsertInstructorsAsync(List<Instructor> instructors);
        Task<IEnumerable<Instructor>> GetInstructorsWithModulesAsync();
        Task<int> GetInstructorsCountAsync();
        Task<IEnumerable<int>> GetDistinctHireYearsAsync();
        Task<IEnumerable<object>> GetModuleCountPerInstructorAsync();
        Task<bool> InstructorExistsAsync(long id);
    }
}
