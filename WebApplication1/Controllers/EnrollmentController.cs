using Microsoft.AspNetCore.Mvc;
using WebApplication1.Application.IServices;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEnrollments()
        {
            var enrollments = await _enrollmentService.GetEnrollmentsAsync();
            return Ok(enrollments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollment(Enrollment enrollment)
        {
            var createdEnrollment = await _enrollmentService.CreateEnrollmentAsync(enrollment);
            if (createdEnrollment == null)
            {
                return BadRequest("Enrollment failed. Invalid student/course or duplicate enrollment.");
            }

            return Ok(createdEnrollment);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEnrollment(Enrollment enrollment)
        {
            var success = await _enrollmentService.DeleteEnrollmentAsync(enrollment.StudentId, enrollment.CourseId);
            if (!success)
            {
                return NotFound("Enrollment not found.");
            }

            return NoContent();
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkCreateEnrollments(List<Enrollment> enrollments)
        {
            var count = await _enrollmentService.BulkCreateEnrollmentsAsync(enrollments);
            return Ok($"{count} enrollments added successfully.");
        }

        [HttpGet("full-details")]
        public async Task<IActionResult> GetEnrollmentsFullDetails()
        {
            var data = await _enrollmentService.GetEnrollmentsFullDetailsAsync();
            return Ok(data);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetEnrollmentsCount()
        {
            var count = await _enrollmentService.GetEnrollmentsCountAsync();
            return Ok(count);
        }

        [HttpGet("by-date")]
        public async Task<IActionResult> GetEnrollmentsByDate([FromQuery] DateTime date)
        {
            var data = await _enrollmentService.GetEnrollmentsByDateAsync(date);
            return Ok(data);
        }
    }
}