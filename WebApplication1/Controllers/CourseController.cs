using Microsoft.AspNetCore.Mvc;
using WebApplication1.Application.IServices;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: /api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetCourses()
        {
            var courses = await _courseService.GetAllCoursesWithModuleCountAsync();
            return Ok(courses);
        }

        // GET: /api/courses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(long id)
        {
            var course = await _courseService.GetCourseWithModulesAsync(id);
            if (course == null)
            {
                return NotFound($"Course with {id} id not found.");
            }
            return Ok(course);
        }

        // GET: /api/courses/{id}/modules
        [HttpGet("{id}/modules")]
        public async Task<ActionResult<IEnumerable<Module>>> GetModulesByCourse(long id)
        {
            var modules = await _courseService.GetModulesByCourseIdAsync(id);

            if (modules == null)
            {
                return NotFound($"Course with {id} id not found.");
            }

            return Ok(modules);
        }

        // GET: /api/courses/{id}/students
        [HttpGet("{id}/students")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByCourse(long id)
        {
            var students = await _courseService.GetStudentsByCourseIdAsync(id);

            if (students == null)
            {
                return NotFound($"Course with {id} id not found.");
            }

            return Ok(students);
        }

        // POST: /api/courses
        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse(Course course)
        {
            var createdCourse = await _courseService.CreateCourseAsync(course);

            return CreatedAtAction(nameof(GetCourse), new { id = createdCourse.Id }, createdCourse);
        }

        // POST: /api/courses/{id}/modules
        [HttpPost("{id}/modules")]
        public async Task<ActionResult<Module>> AddModuleToCourse(long id, Module module)
        {
            var addedModule = await _courseService.AddModuleToCourseAsync(id, module);

            if (addedModule == null)
            {
                return NotFound($"Course with {id} id not found.");
            }

            return Ok(addedModule);
        }

        // PUT: /api/courses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(long id, Course updatedCourse)
        {
            var success = await _courseService.UpdateCourseAsync(id, updatedCourse);

            if (!success)
            {
                return BadRequest("Update failed. Course might not exist or ID mismatch.");
            }

            return NoContent();
        }

        // DELETE: /api/courses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(long id)
        {
            var success = await _courseService.DeleteCourseAsync(id);
            
            if (!success)
            {
                return NotFound($"Course with {id} id not found.");
            }

            return NoContent();
        }

        // POST: /api/courses/bulk
        [HttpPost("bulk")]
        public async Task<IActionResult> BulkInsertCourses(List<Course> courses)
        {
            var count = await _courseService.BulkInsertCoursesAsync(courses);
            return Ok($"{count} courses added successfully.");
        }

        // GET: /api/courses/with-details
        [HttpGet("with-details")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesWithDetails()
        {
            var courses = await _courseService.GetCoursesWithDetailsAsync();
            return Ok(courses);
        }

        // GET: /api/courses/count
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetCoursesCount()
        {
            var count = await _courseService.GetCoursesCountAsync();
            return Ok(count);
        }

        // GET: /api/courses/total-credits
        [HttpGet("total-credits")]
        public async Task<ActionResult<int>> GetTotalCredits()
        {
            var totalCredits = await _courseService.GetTotalCreditsAsync();
            return Ok(totalCredits);
        }

        // GET: /api/courses/top-enrolled
        [HttpGet("top-enrolled")]
        public async Task<ActionResult<IEnumerable<Course>>> GetTopEnrolledCourses()
        {
            var courses = await _courseService.GetTopEnrolledCoursesAsync();
            return Ok(courses);
        }
    }
}