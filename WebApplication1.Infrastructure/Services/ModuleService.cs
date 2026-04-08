using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Application.IServices;
using WebApplication1.Domain.Models;

namespace WebApplication1.Infrastructure.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleService(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<IEnumerable<Module>> GetModulesAsync()
        {
            return await _moduleRepository.GetModulesAsync();
        }

        public async Task<Module> GetModuleByIdAsync(long id)
        {
            return await _moduleRepository.GetModuleByIdAsync(id);
        }

        public async Task<IEnumerable<Instructor>> GetInstructorsByModuleIdAsync(long id)
        {
            return await _moduleRepository.GetInstructorsByModuleIdAsync(id);
        }

        public async Task<Module> CreateModuleAsync(Module module)
        {
            if (!await _moduleRepository.CourseExistsAsync(module.CourseId))
            {
                return null;
            }

            return await _moduleRepository.CreateModuleAsync(module);
        }

        public async Task<bool> UpdateModuleAsync(long id, Module module)
        {
            if (id != module.Id)
            {
                return false;
            }

            if (!await _moduleRepository.ModuleExistsAsync(id))
            {
                return false;
            }

            if (!await _moduleRepository.CourseExistsAsync(module.CourseId))
            {
                return false;
            }

            return await _moduleRepository.UpdateModuleAsync(module);
        }

        public async Task<bool> DeleteModuleAsync(long id)
        {
            return await _moduleRepository.DeleteModuleAsync(id);
        }

        public async Task<int> BulkInsertModulesAsync(List<Module> modules)
        {
            return await _moduleRepository.BulkInsertModulesAsync(modules);
        }

        public async Task<IEnumerable<Module>> GetModulesWithCourseAsync()
        {
            return await _moduleRepository.GetModulesWithCourseAsync();
        }

        public async Task<int> GetModulesCountAsync()
        {
            return await _moduleRepository.GetModulesCountAsync();
        }

        public async Task<IEnumerable<Module>> GetHighCreditModulesAsync(int minCredits)
        {
            return await _moduleRepository.GetHighCreditModulesAsync(minCredits);
        }

        public async Task<bool> BulkUpdateModuleCreditsAsync(List<Module> modules)
        {
            return await _moduleRepository.BulkUpdateModuleCreditsAsync(modules);
        }
    }
}
