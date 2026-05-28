using AutoMapper;
using Domain.Interfaces.Auth;
using Domain.Models;
using GreenShare.Controllers;
using GreenShare.DTOs.RequestDto.Auth;
using GreenShare.DTOs.ResponseDto.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GreenShare.API.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController :BaseApiController<AuthController>
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            await _service.RegisterAsync(dto.Name, dto.Email, dto.Password, dto.Role,dto.CompanyType);
            return Ok("Registration successful. Check your email.");
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            await _service.ConfirmEmailAsync(token);
            return Ok("Email confirmed successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var token = await _service.LoginAsync(dto.Email, dto.Password);
            return Ok(token);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.ChangePasswordAsync(userId, dto.CurrentPassword, dto.NewPassword, dto.ConfirmPassword);
            return Ok("Password changed successfully.");
        }
    }
}
