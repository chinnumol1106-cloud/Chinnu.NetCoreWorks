using AutoMapper;
using Domain.Interfaces.Auth;
using Domain.Models;
using GreenShare.Controllers;
using GreenShare.DTOs.RequestDto.Auth;
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
        private readonly IMapper _mapper;

        public AuthController(IAuthService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            var user = _mapper.Map<User>(dto);
            await _service.RegisterAsync(user);
            return Ok("Registration successful. Check your email.");
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            await _service.ConfirmEmailAsync(token);
            return Ok("Email confirmed.");
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


            if(dto.NewPassword != dto.ConfirmNewPassword)
            return BadRequest(new { message = "Newpassword and Confirm password do not match" });


            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.ChangePasswordAsync(userId, dto.CurrentPassword, dto.NewPassword);
            return Ok("Password changed");
        }
    }
}
