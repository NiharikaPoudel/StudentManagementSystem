using WebApplication1.Application.DTOs;

namespace WebApplication1.Application.IServices
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterStudentAsync(RegisterRequestDto request);
        Task<AuthResponseDto> RegisterInstructorAsync(RegisterRequestDto request);
        Task<AuthResponseDto> CreateAdminAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    }
}