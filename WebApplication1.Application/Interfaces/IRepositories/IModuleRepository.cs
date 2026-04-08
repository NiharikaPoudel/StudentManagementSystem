using WebApplication1.Domain.Models;

namespace WebApplication1.Application.Interfaces.IRepositories
{
    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> GetModulesAsync();
        Task<Module> GetModuleByIdAsync(long id);
        Task<IEnumerable<Instructor>> GetInstructorsByModuleIdAsync(long id);
        Task<Module> CreateModuleAsync(Module module);
        Task<bool> UpdateModuleAsync(Module module);
        Task<bool> DeleteModuleAsync(long id);
        Task<int> BulkInsertModulesAsync(List<Module> modules);
        Task<IEnumerable<Module>> GetModulesWithCourseAsync();
        Task<int> GetModulesCountAsync();
        Task<IEnumerable<Module>> GetHighCreditModulesAsync(int minCredits);
        Task<bool> BulkUpdateModuleCreditsAsync(List<Module> modules);
        Task<bool> ModuleExistsAsync(long id);
        Task<bool> CourseExistsAsync(long courseId);
    }
}
