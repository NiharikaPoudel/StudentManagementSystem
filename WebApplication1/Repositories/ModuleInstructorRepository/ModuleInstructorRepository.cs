using Microsoft.EntityFrameworkCore;
using CollegeManagementSystem;

namespace WebApplication1.Repositories.ModuleInstructorRepository
{
    public class ModuleInstructorRepository : IModuleInstructorRepository
    {
        private readonly AppDbContext _context;

        public ModuleInstructorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ModuleInstructor>> GetAllAsync()
        {
            return await _context.ModuleInstructor.ToListAsync();
        }

        public async Task<ModuleInstructor> GetByIdsAsync(long moduleId, long instructorId)
        {
            return await _context.ModuleInstructor.FindAsync(moduleId, instructorId);
        }

        public async Task<ModuleInstructor> CreateAsync(ModuleInstructor moduleInstructor)
        {
            _context.ModuleInstructor.Add(moduleInstructor);
            await _context.SaveChangesAsync();
            return moduleInstructor;
        }

        public async Task<bool> UpdateAsync(ModuleInstructor moduleInstructor)
        {
            _context.Entry(moduleInstructor).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(long moduleId, long instructorId)
        {
            var moduleInstructor = await _context.ModuleInstructor.FindAsync(moduleId, instructorId);
            if (moduleInstructor == null) return false;

            _context.ModuleInstructor.Remove(moduleInstructor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(long moduleId, long instructorId)
        {
            return await _context.ModuleInstructor.AnyAsync(mi => mi.ModuleId == moduleId && mi.InstructorId == instructorId);
        }
    }
}