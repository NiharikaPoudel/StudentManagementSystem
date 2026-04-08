using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Application.IServices;
using WebApplication1.Domain.Models;

namespace WebApplication1.Infrastructure.Services
{
    public class ModuleInstructorService : IModuleInstructorService
    {
        private readonly IModuleInstructorRepository _moduleInstructorRepository;

        public ModuleInstructorService(IModuleInstructorRepository moduleInstructorRepository)
        {
            _moduleInstructorRepository = moduleInstructorRepository;
        }

        public async Task<ModuleInstructor> CreateAssignmentAsync(ModuleInstructor assignment)
        {
            if (!await _moduleInstructorRepository.ModuleExistsAsync(assignment.ModuleId))
            {
                return null;
            }

            if (!await _moduleInstructorRepository.InstructorExistsAsync(assignment.InstructorId))
            {
                return null;
            }

            if (await _moduleInstructorRepository.AssignmentExistsAsync(assignment.ModuleId, assignment.InstructorId))
            {
                return null;
            }

            return await _moduleInstructorRepository.CreateAssignmentAsync(assignment);
        }

        public async Task<bool> DeleteAssignmentAsync(long moduleId, long instructorId)
        {
            return await _moduleInstructorRepository.DeleteAssignmentAsync(moduleId, instructorId);
        }

        public async Task<int> BulkCreateAssignmentsAsync(List<ModuleInstructor> assignments)
        {
            return await _moduleInstructorRepository.BulkCreateAssignmentsAsync(assignments);
        }

        public async Task<IEnumerable<ModuleInstructor>> GetAssignmentsFullDetailsAsync()
        {
            return await _moduleInstructorRepository.GetAssignmentsFullDetailsAsync();
        }

        public async Task<int> GetAssignmentsCountAsync()
        {
            return await _moduleInstructorRepository.GetAssignmentsCountAsync();
        }

        public async Task<IEnumerable<object>> GetInstructorWiseModuleCountAsync()
        {
            return await _moduleInstructorRepository.GetInstructorWiseModuleCountAsync();
        }
    }
}