using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using LMS.Backend.DTOs;
using LMS.Backend.Services;

namespace LMS.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid model state" });

            var (success, message, data) = await _authService.RegisterAsync(model);

            if (!success)
                return BadRequest(new { success = false, message });

            return Ok(new { success = true, message, data });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid model state" });

            var (success, message, data) = await _authService.LoginAsync(model);

            if (!success)
                return Unauthorized(new { success = false, message });

            return Ok(new { success = true, message, data });
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var (success, message) = await _authService.UpdateProfileAsync(userId, model);

            if (!success)
                return BadRequest(new { success = false, message });

            return Ok(new { success = true, message });
        }

        [Authorize]
        [HttpGet("current-user")]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var fullName = User.FindFirst(ClaimTypes.Name)?.Value;
            var role = User.FindFirst("Role")?.Value;

            return Ok(new
            {
                id = userId,
                email = email,
                fullName = fullName,
                role = role
            });
        }
    }
}
