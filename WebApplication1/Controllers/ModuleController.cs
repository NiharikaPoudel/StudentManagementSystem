using Microsoft.AspNetCore.Mvc;
using WebApplication1.Application.IServices;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/modules")]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetModules()
        {
            var modules = await _moduleService.GetModulesAsync();
            return Ok(modules);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModule(long id)
        {
            var module = await _moduleService.GetModuleByIdAsync(id);
            if (module == null)
            {
                return NotFound($"Module with {id} id not found.");
            }

            return Ok(module);
        }

        [HttpGet("{id}/instructors")]
        public async Task<IActionResult> GetInstructorsByModule(long id)
        {
            var instructors = await _moduleService.GetInstructorsByModuleIdAsync(id);
            if (instructors == null)
            {
                return NotFound($"Module with {id} id not found.");
            }

            return Ok(instructors);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModule(Module module)
        {
            var createdModule = await _moduleService.CreateModuleAsync(module);
            if (createdModule == null)
            {
                return BadRequest($"Course with {module.CourseId} id not found.");
            }

            return CreatedAtAction(nameof(GetModule), new { id = createdModule.Id }, createdModule);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModule(long id, Module module)
        {
            var success = await _moduleService.UpdateModuleAsync(id, module);
            if (!success)
            {
                return BadRequest("Update failed. Module might not exist, ID mismatch, or invalid CourseId.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(long id)
        {
            var success = await _moduleService.DeleteModuleAsync(id);
            if (!success)
            {
                return NotFound($"Module with {id} id not found.");
            }

            return NoContent();
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkInsertModules(List<Module> modules)
        {
            var count = await _moduleService.BulkInsertModulesAsync(modules);
            return Ok($"{count} modules added successfully.");
        }

        [HttpGet("with-course")]
        public async Task<IActionResult> GetModulesWithCourse()
        {
            var modules = await _moduleService.GetModulesWithCourseAsync();
            return Ok(modules);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetModulesCount()
        {
            var count = await _moduleService.GetModulesCountAsync();
            return Ok(count);
        }

        [HttpGet("high-credit")]
        public async Task<IActionResult> GetHighCreditModules([FromQuery] int minCredits = 3)
        {
            var modules = await _moduleService.GetHighCreditModulesAsync(minCredits);
            return Ok(modules);
        }

        [HttpPut("bulk-update-credits")]
        public async Task<IActionResult> BulkUpdateModuleCredits(List<Module> modules)
        {
            var success = await _moduleService.BulkUpdateModuleCreditsAsync(modules);
            if (!success)
            {
                return BadRequest("Bulk update failed.");
            }

            return Ok("Module credits updated successfully.");
        }
    }
}