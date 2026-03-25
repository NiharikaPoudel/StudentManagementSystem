using Microsoft.EntityFrameworkCore;
using CollegeManagementSystem;

namespace WebApplication1.Repositories.InstructorRepository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AppDbContext _context;

        public InstructorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await _context.Instructor.ToListAsync();
        }

        public async Task<Instructor> GetByIdAsync(long id)
        {
            return await _context.Instructor.FindAsync(id);
        }

        public async Task<Instructor> CreateAsync(Instructor instructor)
        {
            _context.Instructor.Add(instructor);
            await _context.SaveChangesAsync();
            return instructor;
        }

        public async Task<bool> UpdateAsync(Instructor instructor)
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

        public async Task<bool> DeleteAsync(long id)
        {
            var instructor = await _context.Instructor.FindAsync(id);
            if (instructor == null) return false;

            _context.Instructor.Remove(instructor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Instructor.AnyAsync(i => i.Id == id);
        }
    }
}