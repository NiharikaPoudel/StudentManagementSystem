using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services.StudentServices;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: /api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _studentService.GetStudentsAsync();
            return Ok(students);
        }

        // GET: /api/students/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(long id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Student with {id} id not found.");
            }
            return Ok(student);
        }

        // GET: /api/students/{id}/courses
        [HttpGet("{id}/courses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByStudent(long id)
        {
            var courses = await _studentService.GetCoursesByStudentIdAsync(id);

            if (courses == null)
            {
                return NotFound($"Student with {id} id not found.");
            }

            return Ok(courses);
        }

        // POST: /api/students
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            var createdStudent = await _studentService.CreateStudentAsync(student);

            return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
        }

        // PUT: /api/students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(long id, Student updatedStudent)
        {
            var success = await _studentService.UpdateStudentAsync(id, updatedStudent);

            if (!success)
            {
                return BadRequest("Update failed. Student might not exist or ID mismatch.");
            }

            return NoContent();
        }

        // DELETE: /api/students/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(long id)
        {
            var success = await _studentService.DeleteStudentAsync(id);
            
            if (!success)
            {
                return NotFound($"Student with {id} id not found.");
            }

            return NoContent();
        }

        // POST: /api/students/bulk
        [HttpPost("bulk")]
        public async Task<IActionResult> BulkInsertStudents(List<Student> students)
        {
            var count = await _studentService.BulkInsertStudentsAsync(students);
            return Ok($"{count} students added successfully.");
        }

        // GET: /api/students/with-courses
        [HttpGet("with-courses")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsWithCourses()
        {
            var students = await _studentService.GetStudentsWithCoursesAsync();
            return Ok(students);
        }

        // GET: /api/students/count
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetStudentsCount()
        {
            var count = await _studentService.GetStudentsCountAsync();
            return Ok(count);
        }

        // GET: /api/students/full-details
        [HttpGet("full-details")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsFullDetails()
        {
            var students = await _studentService.GetStudentsFullDetailsAsync();
            return Ok(students);
        }
    }
}
