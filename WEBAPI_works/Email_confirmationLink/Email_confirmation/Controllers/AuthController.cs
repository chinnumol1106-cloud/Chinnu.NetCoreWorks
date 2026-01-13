using Email_confirmation.Helper;
using Email_confirmation.Models;
using Email_confirmation.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;

namespace Email_confirmation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IEmailService _emailService;

        public AuthController(AppDbContext db, IEmailService emailService)
        {
            _db = db;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            // 1. Create token
            var token = Guid.NewGuid().ToString();

            // 2. Create user
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                EmailConfirmationToken = token,
                EmailConfirmed = false
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            // 3. Build confirmation link
            var confirmLink =
                $"https://localhost:5001/api/auth/confirm-email?token={token}";

            // 4. Send email
            MailRequest mailrequest = new MailRequest();
            mailrequest.ToEmail = user.Email;
            mailrequest.Subject = "Confirm your email";
            mailrequest.Body = $"Click this link to confirm your email: <br/> <a href='{confirmLink}'>Confirm Email</a>";
            await _emailService.SendEmailAsync(mailrequest);

            return Ok("Registration successful. Check your email to confirm.");
        }


        [HttpGet("confirm-email")]
        public IActionResult ConfirmEmail(string token)
        {
            var user = _db.Users.FirstOrDefault(u => u.EmailConfirmationToken == token);

            if (user == null)
                return BadRequest("Invalid token");

            user.EmailConfirmed = true;
            user.EmailConfirmationToken = null;

            _db.SaveChanges();

            return Ok("Email confirmed successfully. You can login now.");
        }


        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
                return Unauthorized("Invalid credentials");

            if (!user.EmailConfirmed)
                return Unauthorized("Please confirm your email first");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            return Ok("Login success");
        }


    }

}
