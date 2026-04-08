using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Application.IServices;
using WebApplication1.Domain.Models;

namespace WebApplication1.Infrastructure.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorService(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public async Task<IEnumerable<Instructor>> GetInstructorsAsync()
        {
            return await _instructorRepository.GetInstructorsAsync();
        }

        public async Task<Instructor> GetInstructorByIdAsync(long id)
        {
            return await _instructorRepository.GetInstructorByIdAsync(id);
        }

        public async Task<Instructor> CreateInstructorAsync(Instructor instructor)
        {
            return await _instructorRepository.CreateInstructorAsync(instructor);
        }

        public async Task<bool> UpdateInstructorAsync(long id, Instructor instructor)
        {
            if (id != instructor.Id)
            {
                return false;
            }

            if (!await _instructorRepository.InstructorExistsAsync(id))
            {
                return false;
            }

            return await _instructorRepository.UpdateInstructorAsync(instructor);
        }

        public async Task<bool> DeleteInstructorAsync(long id)
        {
            return await _instructorRepository.DeleteInstructorAsync(id);
        }

        public async Task<int> BulkInsertInstructorsAsync(List<Instructor> instructors)
        {
            return await _instructorRepository.BulkInsertInstructorsAsync(instructors);
        }

        public async Task<IEnumerable<Instructor>> GetInstructorsWithModulesAsync()
        {
            return await _instructorRepository.GetInstructorsWithModulesAsync();
        }

        public async Task<int> GetInstructorsCountAsync()
        {
            return await _instructorRepository.GetInstructorsCountAsync();
        }

        public async Task<IEnumerable<int>> GetDistinctHireYearsAsync()
        {
            return await _instructorRepository.GetDistinctHireYearsAsync();
        }

        public async Task<IEnumerable<object>> GetModuleCountPerInstructorAsync()
        {
            return await _instructorRepository.GetModuleCountPerInstructorAsync();
        }
    }
}
