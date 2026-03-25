using Microsoft.EntityFrameworkCore;
using CollegeManagementSystem;

namespace WebApplication1.Repositories.ModuleRepository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly AppDbContext _context;

        public ModuleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Module>> GetAllAsync()
        {
            return await _context.Module.ToListAsync();
        }

        public async Task<Module> GetByIdAsync(long id)
        {
            return await _context.Module.FindAsync(id);
        }

        public async Task<Module> CreateAsync(Module module)
        {
            _context.Module.Add(module);
            await _context.SaveChangesAsync();
            return module;
        }

        public async Task<bool> UpdateAsync(Module module)
        {
            _context.Entry(module).State = EntityState.Modified;
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

        public async Task<bool> DeleteAsync(long id)
        {
            var module = await _context.Module.FindAsync(id);
            if (module == null) return false;

            _context.Module.Remove(module);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Module.AnyAsync(m => m.Id == id);
        }
    }
}