using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Application.IServices;
using WebApplication1.Domain.Models;

namespace WebApplication1.Infrastructure.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsAsync()
        {
            return await _enrollmentRepository.GetEnrollmentsAsync();
        }

        public async Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment)
        {
            if (!await _enrollmentRepository.StudentExistsAsync(enrollment.StudentId))
            {
                return null;
            }

            if (!await _enrollmentRepository.CourseExistsAsync(enrollment.CourseId))
            {
                return null;
            }

            if (await _enrollmentRepository.EnrollmentExistsAsync(enrollment.StudentId, enrollment.CourseId))
            {
                return null;
            }

            return await _enrollmentRepository.CreateEnrollmentAsync(enrollment);
        }

        public async Task<bool> DeleteEnrollmentAsync(long studentId, long courseId)
        {
            return await _enrollmentRepository.DeleteEnrollmentAsync(studentId, courseId);
        }

        public async Task<int> BulkCreateEnrollmentsAsync(List<Enrollment> enrollments)
        {
            return await _enrollmentRepository.BulkCreateEnrollmentsAsync(enrollments);
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsFullDetailsAsync()
        {
            return await _enrollmentRepository.GetEnrollmentsFullDetailsAsync();
        }

        public async Task<int> GetEnrollmentsCountAsync()
        {
            return await _enrollmentRepository.GetEnrollmentsCountAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByDateAsync(DateTime date)
        {
            return await _enrollmentRepository.GetEnrollmentsByDateAsync(date);
        }
    }
}
