using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Domain.Models;
using WebApplication1.Infrastructure.Presistance;

namespace WebApplication1.Infrastructure.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AppDbContext _context;

        public InstructorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Instructor>> GetInstructorsAsync()
        {
            return await _context.Instructor.ToListAsync();
        }

        public async Task<Instructor> GetInstructorByIdAsync(long id)
        {
            return await _context.Instructor.FindAsync(id);
        }

        public async Task<Instructor> CreateInstructorAsync(Instructor instructor)
        {
            _context.Instructor.Add(instructor);
            await _context.SaveChangesAsync();
            return instructor;
        }

        public async Task<bool> UpdateInstructorAsync(Instructor instructor)
        {
            _context.Entry(instructor).State = EntityState.Modified;
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

        public async Task<bool> DeleteInstructorAsync(long id)
        {
            var instructor = await _context.Instructor.FindAsync(id);
            if (instructor == null) return false;

            _context.Instructor.Remove(instructor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> BulkInsertInstructorsAsync(List<Instructor> instructors)
        {
            _context.Instructor.AddRange(instructors);
            await _context.SaveChangesAsync();
            return instructors.Count;
        }

        public async Task<IEnumerable<Instructor>> GetInstructorsWithModulesAsync()
        {
            return await _context.Instructor
                .Include(i => i.ModuleInstructors)
                .ThenInclude(mi => mi.Module)
                .ToListAsync();
        }

        public async Task<int> GetInstructorsCountAsync()
        {
            return await _context.Instructor.CountAsync();
        }

        public async Task<IEnumerable<int>> GetDistinctHireYearsAsync()
        {
            return await _context.Instructor
                .Select(i => i.HireDate.Year)
                .Distinct()
                .OrderBy(y => y)
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetModuleCountPerInstructorAsync()
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

        public async Task<bool> InstructorExistsAsync(long id)
        {
            return await _context.Instructor.AnyAsync(i => i.Id == id);
        }
    }
}
