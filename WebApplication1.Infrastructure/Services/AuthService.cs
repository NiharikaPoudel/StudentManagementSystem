using Microsoft.AspNetCore.Identity;
using WebApplication1.Application.DTOs;
using WebApplication1.Application.IServices;
using WebApplication1.Domain.Models;

namespace WebApplication1.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<AuthResponseDto> RegisterStudentAsync(RegisterRequestDto request)
        {
            return await RegisterByRoleAsync(request, "Student");
        }

        public async Task<AuthResponseDto> RegisterInstructorAsync(RegisterRequestDto request)
        {
            return await RegisterByRoleAsync(request, "Instructor");
        }

        public async Task<AuthResponseDto> CreateAdminAsync(RegisterRequestDto request)
        {
            return await RegisterByRoleAsync(request, "Admin");
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            var signIn = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signIn.Succeeded)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponseDto
            {
                Success = true,
                Message = "Login successful.",
                Email = user.Email,
                Roles = roles
            };
        }

        private async Task<AuthResponseDto> RegisterByRoleAsync(RegisterRequestDto request, string role)
        {
            await EnsureRoleExistsAsync(role);

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "User already exists with this email."
                };
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = string.Join(" | ", createResult.Errors.Select(e => e.Description))
                };
            }

            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = string.Join(" | ", roleResult.Errors.Select(e => e.Description))
                };
            }

            return new AuthResponseDto
            {
                Success = true,
                Message = $"{role} registered successfully.",
                Email = user.Email,
                Roles = new[] { role }
            };
        }

        private async Task EnsureRoleExistsAsync(string role)
        {
            var exists = await _roleManager.RoleExistsAsync(role);
            if (!exists)
            {
                await _roleManager.CreateAsync(new ApplicationRole
                {
                    Name = role,
                    NormalizedName = role.ToUpperInvariant()
                });
            }
        }
    }
}