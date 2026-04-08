using Microsoft.AspNetCore.Mvc;
using WebApplication1.Application.IServices;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/moduleinstructors")]
    public class ModuleInstructorController : ControllerBase
    {
        private readonly IModuleInstructorService _moduleInstructorService;

        public ModuleInstructorController(IModuleInstructorService moduleInstructorService)
        {
            _moduleInstructorService = moduleInstructorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignment(ModuleInstructor assignment)
        {
            var createdAssignment = await _moduleInstructorService.CreateAssignmentAsync(assignment);
            if (createdAssignment == null)
            {
                return BadRequest("Assignment failed. Invalid module/instructor or duplicate assignment.");
            }

            return Ok(createdAssignment);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAssignment(ModuleInstructor assignment)
        {
            var success = await _moduleInstructorService.DeleteAssignmentAsync(assignment.ModuleId, assignment.InstructorId);
            if (!success)
            {
                return NotFound("Assignment not found.");
            }

            return NoContent();
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkCreateAssignments(List<ModuleInstructor> assignments)
        {
            var count = await _moduleInstructorService.BulkCreateAssignmentsAsync(assignments);
            return Ok($"{count} assignments added successfully.");
        }

        [HttpGet("full-details")]
        public async Task<IActionResult> GetAssignmentsFullDetails()
        {
            var data = await _moduleInstructorService.GetAssignmentsFullDetailsAsync();
            return Ok(data);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetAssignmentsCount()
        {
            var count = await _moduleInstructorService.GetAssignmentsCountAsync();
            return Ok(count);
        }

        [HttpGet("module-count")]
        public async Task<IActionResult> GetInstructorWiseModuleCount()
        {
            var data = await _moduleInstructorService.GetInstructorWiseModuleCountAsync();
            return Ok(data);
        }
    }
}