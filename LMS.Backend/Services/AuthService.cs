using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using LMS.Backend.DTOs;
using LMS.Backend.Models;
using Microsoft.Extensions.Configuration;

namespace LMS.Backend.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, LoginResponseDto Data)> RegisterAsync(RegisterDto model);
        Task<(bool Success, string Message, LoginResponseDto Data)> LoginAsync(LoginDto model);
        Task<(bool Success, string Message)> UpdateProfileAsync(string userId, UpdateProfileDto model);
        string GenerateJwtToken(User user);
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<(bool Success, string Message, LoginResponseDto Data)> RegisterAsync(RegisterDto model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return (false, "Passwords do not match.", null);
            }

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                Role = model.Role ?? "Student"
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors);
                return (false, $"Registration failed: {errors}", null);
            }

            var token = GenerateJwtToken(user);
            var userDto = MapUserToDto(user);

            return (true, "Registration successful.", new LoginResponseDto 
            { 
                Token = token, 
                User = userDto 
            });
        }

        public async Task<(bool Success, string Message, LoginResponseDto Data)> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return (false, "Invalid credentials.", null);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                return (false, "Invalid credentials.", null);
            }

            var token = GenerateJwtToken(user);
            var userDto = MapUserToDto(user);

            return (true, "Login successful.", new LoginResponseDto 
            { 
                Token = token, 
                User = userDto 
            });
        }

        public async Task<(bool Success, string Message)> UpdateProfileAsync(string userId, UpdateProfileDto model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (false, "User not found.");
            }

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors);
                return (false, $"Update failed: {errors}");
            }

            return (true, "Profile updated successfully.");
        }

        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationMinutes"])),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserDto MapUserToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
