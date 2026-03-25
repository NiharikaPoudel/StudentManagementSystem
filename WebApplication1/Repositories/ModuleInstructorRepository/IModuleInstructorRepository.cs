namespace WebApplication1.Repositories.ModuleInstructorRepository
{
    public interface IModuleInstructorRepository
    {
        Task<IEnumerable<ModuleInstructor>> GetAllAsync();
        Task<ModuleInstructor> GetByIdsAsync(long moduleId, long instructorId);
        Task<ModuleInstructor> CreateAsync(ModuleInstructor moduleInstructor);
        Task<bool> UpdateAsync(ModuleInstructor moduleInstructor);
        Task<bool> DeleteAsync(long moduleId, long instructorId);
        Task<bool> ExistsAsync(long moduleId, long instructorId);
    }
}