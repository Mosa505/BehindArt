using BehindArt.Application.DTOs;
using BehindArt.Application.Interfaces;
using BehindArt.Domain.Entitiyes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, ITokenService tokenService, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);  
            if (user is null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!passwordValid)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var roles = await _userManager.GetRolesAsync(user);
            var token = await _tokenService.CreateTokenAsync(user, (List<string>)roles);

            return new AuthResponseDto
            {
                Id = user.Id,
                Username = user.UserName!,
                Email = user.Email!,
                Role = roles.FirstOrDefault() ?? "User",
                Token = token
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser is not null)
                throw new InvalidOperationException("An account with this email already exists.");
            var user = new User
            {
                UserName = dto.Username,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(" ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException(errors);
            }
            await _userManager.AddToRoleAsync(user, "User");

            var roles = await _userManager.GetRolesAsync(user);
            var token = await _tokenService.CreateTokenAsync(user, (List<string>)roles);

            return new AuthResponseDto
            {
                Id = user.Id,
                Username = user.UserName!,
                Email = user.Email!,
                Role = roles.FirstOrDefault() ?? "User",
                Token = token
            };
        }

        public async Task AdminAsync(AdminDto dto)
        {
            var expectedKey = _configuration["AdminSetup:SecretKey"];

            if (dto.SecretKey != expectedKey)
                throw new UnauthorizedAccessException("Invalid secret key.");

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
                throw new KeyNotFoundException("User not found.");

            var isAlreadyAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (isAlreadyAdmin)
                throw new InvalidOperationException("This user is already an Admin.");

            await _userManager.AddToRoleAsync(user, "Admin");
        }
        public async Task UpdateUserRoleAsync(int userId, string newRole)
        {
            if (newRole != "Admin" && newRole != "User")
                throw new ArgumentException("Role must be either 'Admin' or 'User'.");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new KeyNotFoundException("User not found.");

            var currentRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, newRole);
        }
    }
    }

