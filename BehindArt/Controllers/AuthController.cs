using BehindArt.Application.DTOs;
using BehindArt.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BehindArt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }
        [HttpPost("FromUserToAdmin")]
        public async Task<IActionResult> Admin([FromBody] AdminDto dto)
        {
            await _authService.AdminAsync(dto);
            return Ok(new { message = "User to Admin successfully." });
        }

        [HttpPut("users/{userId:int}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRole(int userId, [FromBody] UpdateUserRoleDto dto)
        {
            await _authService.UpdateUserRoleAsync(userId, dto.Role);
            return Ok(new { message = $"User role updated to {dto.Role}." });
        }
    }
}
