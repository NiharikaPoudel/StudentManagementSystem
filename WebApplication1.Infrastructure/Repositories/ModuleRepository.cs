using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Domain.Models;
using WebApplication1.Infrastructure.Presistance;

namespace WebApplication1.Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly AppDbContext _context;

        public ModuleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Module>> GetModulesAsync()
        {
            return await _context.Module.ToListAsync();
        }

        public async Task<Module> GetModuleByIdAsync(long id)
        {
            return await _context.Module.FindAsync(id);
        }

        public async Task<IEnumerable<Instructor>> GetInstructorsByModuleIdAsync(long id)
        {
            var module = await _context.Module
                .Include(m => m.ModuleInstructors)
                .ThenInclude(mi => mi.Instructor)
                .FirstOrDefaultAsync(m => m.Id == id);

            return module?.ModuleInstructors.Select(mi => mi.Instructor);
        }

        public async Task<Module> CreateModuleAsync(Module module)
        {
            _context.Module.Add(module);
            await _context.SaveChangesAsync();
            return module;
        }

        public async Task<bool> UpdateModuleAsync(Module module)
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

        public async Task<bool> DeleteModuleAsync(long id)
        {
            var module = await _context.Module.FindAsync(id);
            if (module == null) return false;

            _context.Module.Remove(module);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> BulkInsertModulesAsync(List<Module> modules)
        {
            _context.Module.AddRange(modules);
            await _context.SaveChangesAsync();
            return modules.Count;
        }

        public async Task<IEnumerable<Module>> GetModulesWithCourseAsync()
        {
            return await _context.Module
                .Include(m => m.Course)
                .ToListAsync();
        }

        public async Task<int> GetModulesCountAsync()
        {
            return await _context.Module.CountAsync();
        }

        public async Task<IEnumerable<Module>> GetHighCreditModulesAsync(int minCredits)
        {
            return await _context.Module
                .Where(m => m.Credits > minCredits)
                .ToListAsync();
        }

        public async Task<bool> BulkUpdateModuleCreditsAsync(List<Module> modules)
        {
            var moduleIds = modules.Select(m => m.Id).ToList();
            var existingModules = await _context.Module.Where(m => moduleIds.Contains(m.Id)).ToListAsync();

            foreach (var existingModule in existingModules)
            {
                var incomingModule = modules.FirstOrDefault(m => m.Id == existingModule.Id);
                if (incomingModule != null)
                {
                    existingModule.Credits = incomingModule.Credits;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ModuleExistsAsync(long id)
        {
            return await _context.Module.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> CourseExistsAsync(long courseId)
        {
            return await _context.Course.AnyAsync(c => c.Id == courseId);
        }
    }
}
