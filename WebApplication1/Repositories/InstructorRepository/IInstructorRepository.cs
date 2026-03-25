namespace WebApplication1.Repositories.InstructorRepository
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAllAsync();
        Task<Instructor> GetByIdAsync(long id);
        Task<Instructor> CreateAsync(Instructor instructor);
        Task<bool> UpdateAsync(Instructor instructor);
        Task<bool> DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
    }
}