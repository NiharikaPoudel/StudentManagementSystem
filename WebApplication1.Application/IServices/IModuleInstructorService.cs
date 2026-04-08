using WebApplication1.Domain.Models;

namespace WebApplication1.Application.IServices
{
    public interface IModuleInstructorService
    {
        Task<ModuleInstructor> CreateAssignmentAsync(ModuleInstructor assignment);
        Task<bool> DeleteAssignmentAsync(long moduleId, long instructorId);
        Task<int> BulkCreateAssignmentsAsync(List<ModuleInstructor> assignments);
        Task<IEnumerable<ModuleInstructor>> GetAssignmentsFullDetailsAsync();
        Task<int> GetAssignmentsCountAsync();
        Task<IEnumerable<object>> GetInstructorWiseModuleCountAsync();
    }
}
