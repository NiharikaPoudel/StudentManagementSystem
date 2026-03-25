namespace WebApplication1.Repositories.ModuleRepository
{
    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> GetAllAsync();
        Task<Module> GetByIdAsync(long id);
        Task<Module> CreateAsync(Module module);
        Task<bool> UpdateAsync(Module module);
        Task<bool> DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
    }
}