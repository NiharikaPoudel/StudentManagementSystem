using WebApplication1.Domain.Models;

namespace WebApplication1.Application.Interfaces.IRepositories
{
    public interface IModuleInstructorRepository
    {
        Task<ModuleInstructor> CreateAssignmentAsync(ModuleInstructor moduleInstructor);
        Task<bool> DeleteAssignmentAsync(long moduleId, long instructorId);
        Task<int> BulkCreateAssignmentsAsync(List<ModuleInstructor> moduleInstructors);
        Task<IEnumerable<ModuleInstructor>> GetAssignmentsFullDetailsAsync();
        Task<int> GetAssignmentsCountAsync();
        Task<IEnumerable<object>> GetInstructorWiseModuleCountAsync();
        Task<bool> AssignmentExistsAsync(long moduleId, long instructorId);
        Task<bool> ModuleExistsAsync(long moduleId);
        Task<bool> InstructorExistsAsync(long instructorId);
    }
}
