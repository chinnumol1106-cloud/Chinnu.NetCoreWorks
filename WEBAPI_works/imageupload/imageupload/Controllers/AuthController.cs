using Azure.Core;
using imageupload.Data;
using imageupload.Helper;
using imageupload.Models;
using imageupload.Requestobject.Auth;
using imageupload.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace imageupload.Controllers
{
    [Route("api/Auth")]
    [ApiController]
  
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly ITokenInterface _token;
        private readonly IEmailService _emailService;


        public AuthController(AppDbContext context, ITokenInterface token,IEmailService emailService)
        {
            _context = context;
            _token = token;
            _emailService = emailService;
        }


        [HttpPost("/UserRegister")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest newuser)
        {
            if (_context.Users.Any(x => x.Email == newuser.Email))
                return BadRequest("Email already exists");

            // 1. Create token

            var token = Guid.NewGuid().ToString();

            //Create new User

            var user = new User
            {
                Name = newuser.Name,
                Email = newuser.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(newuser.Password),
                Role = newuser.Role,
                EmailConfirmationToken = token,
                EmailConfirmed = false
            };

            _context.Users.Add(user);
            _context.SaveChanges();


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
            var user = _context.Users.FirstOrDefault(u => u.EmailConfirmationToken == token);

            if (user == null)
                return BadRequest("Invalid token");

            user.EmailConfirmed = true;
            user.EmailConfirmationToken = null;

            _context.SaveChanges();

            return Ok("Email confirmed successfully. You can login now.");
        }



        [HttpPost("login")]
        public IActionResult Login(LoginRequest userlogin)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userlogin.Email);

            if (user == null)
                return Unauthorized("Invalid credentials");

            if (!user.EmailConfirmed)
                return Unauthorized("Please confirm your email first");

            if (!BCrypt.Net.BCrypt.Verify(userlogin.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            if (!user.IsActive)
                return Unauthorized("Your account is blocked by admin");

            string Token = _token.CreateToken(user);
            return Ok(Token);
        }



        [HttpGet("/GetUserName")]
        public ActionResult GetName()
        {
            var username = _token.GetuserName();
            return Ok(username);
        }


        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            // 1️⃣ Validate new password
            if (request.NewPassword != request.ConfirmNewPassword)
                return BadRequest("New password and confirm password do not match");

            // 2️⃣ Get logged-in user id
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User id not found in token");

            var userId = Guid.Parse(userIdClaim);

            // 3️⃣ Fetch user
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return Unauthorized("User not found");

            // 4️⃣ Verify current password
            bool isValid = BCrypt.Net.BCrypt.Verify(
                request.CurrentPassword,
                user.PasswordHash
            );

            if (!isValid)
                return BadRequest("Current password is incorrect");

            // 5️⃣ Update password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            await _context.SaveChangesAsync();

            return Ok("Password changed successfully");
        }
    }


}

