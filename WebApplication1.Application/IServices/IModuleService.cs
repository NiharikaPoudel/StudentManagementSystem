using WebApplication1.Domain.Models;

namespace WebApplication1.Application.IServices
{
    public interface IModuleService
    {
        Task<IEnumerable<Module>> GetModulesAsync();
        Task<Module> GetModuleByIdAsync(long id);
        Task<IEnumerable<Instructor>> GetInstructorsByModuleIdAsync(long id);
        Task<Module> CreateModuleAsync(Module module);
        Task<bool> UpdateModuleAsync(long id, Module module);
        Task<bool> DeleteModuleAsync(long id);
        Task<int> BulkInsertModulesAsync(List<Module> modules);
        Task<IEnumerable<Module>> GetModulesWithCourseAsync();
        Task<int> GetModulesCountAsync();
        Task<IEnumerable<Module>> GetHighCreditModulesAsync(int minCredits);
        Task<bool> BulkUpdateModuleCreditsAsync(List<Module> modules);
    }
}
