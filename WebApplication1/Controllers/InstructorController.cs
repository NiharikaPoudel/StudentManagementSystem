using Microsoft.AspNetCore.Mvc;
using WebApplication1.Application.IServices;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/instructors")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInstructors()
        {
            var instructors = await _instructorService.GetInstructorsAsync();
            return Ok(instructors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstructor(long id)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(id);
            if (instructor == null)
            {
                return NotFound($"Instructor with {id} id not found.");
            }

            return Ok(instructor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstructor(Instructor instructor)
        {
            var createdInstructor = await _instructorService.CreateInstructorAsync(instructor);
            return CreatedAtAction(nameof(GetInstructor), new { id = createdInstructor.Id }, createdInstructor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstructor(long id, Instructor instructor)
        {
            var success = await _instructorService.UpdateInstructorAsync(id, instructor);
            if (!success)
            {
                return BadRequest("Update failed. Instructor might not exist or ID mismatch.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructor(long id)
        {
            var success = await _instructorService.DeleteInstructorAsync(id);
            if (!success)
            {
                return NotFound($"Instructor with {id} id not found.");
            }

            return NoContent();
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkInsertInstructors(List<Instructor> instructors)
        {
            var count = await _instructorService.BulkInsertInstructorsAsync(instructors);
            return Ok($"{count} instructors added successfully.");
        }

        [HttpGet("modules")]
        public async Task<IActionResult> GetInstructorsWithModules()
        {
            var instructors = await _instructorService.GetInstructorsWithModulesAsync();
            return Ok(instructors);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetInstructorsCount()
        {
            var count = await _instructorService.GetInstructorsCountAsync();
            return Ok(count);
        }

        [HttpGet("distinct-hireyear")]
        public async Task<IActionResult> GetDistinctHireYears()
        {
            var years = await _instructorService.GetDistinctHireYearsAsync();
            return Ok(years);
        }

        [HttpGet("module-count")]
        public async Task<IActionResult> GetModuleCountPerInstructor()
        {
            var data = await _instructorService.GetModuleCountPerInstructorAsync();
            return Ok(data);
        }
    }
}