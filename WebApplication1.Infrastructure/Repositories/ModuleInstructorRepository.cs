using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Domain.Models;
using WebApplication1.Infrastructure.Presistance;

namespace WebApplication1.Infrastructure.Repositories
{
    public class ModuleInstructorRepository : IModuleInstructorRepository
    {
        private readonly AppDbContext _context;

        public ModuleInstructorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ModuleInstructor> CreateAssignmentAsync(ModuleInstructor moduleInstructor)
        {
            _context.ModuleInstructor.Add(moduleInstructor);
            await _context.SaveChangesAsync();
            return moduleInstructor;
        }

        public async Task<bool> DeleteAssignmentAsync(long moduleId, long instructorId)
        {
            var moduleInstructor = await _context.ModuleInstructor.FindAsync(moduleId, instructorId);
            if (moduleInstructor == null) return false;

            _context.ModuleInstructor.Remove(moduleInstructor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> BulkCreateAssignmentsAsync(List<ModuleInstructor> moduleInstructors)
        {
            _context.ModuleInstructor.AddRange(moduleInstructors);
            await _context.SaveChangesAsync();
            return moduleInstructors.Count;
        }

        public async Task<IEnumerable<ModuleInstructor>> GetAssignmentsFullDetailsAsync()
        {
            return await _context.ModuleInstructor
                .Include(mi => mi.Module)
                .Include(mi => mi.Instructor)
                .ToListAsync();
        }

        public async Task<int> GetAssignmentsCountAsync()
        {
            return await _context.ModuleInstructor.CountAsync();
        }

        public async Task<IEnumerable<object>> GetInstructorWiseModuleCountAsync()
        {
            return await _context.Instructor
                .Select(i => new
                {
                    i.Id,
                    Name = i.FirstName + " " + i.LastName,
                    ModuleCount = i.ModuleInstructors.Count
                })
                .ToListAsync();
        }

        public async Task<bool> AssignmentExistsAsync(long moduleId, long instructorId)
        {
            return await _context.ModuleInstructor.AnyAsync(mi => mi.ModuleId == moduleId && mi.InstructorId == instructorId);
        }

        public async Task<bool> ModuleExistsAsync(long moduleId)
        {
            return await _context.Module.AnyAsync(m => m.Id == moduleId);
        }

        public async Task<bool> InstructorExistsAsync(long instructorId)
        {
            return await _context.Instructor.AnyAsync(i => i.Id == instructorId);
        }
    }
}
