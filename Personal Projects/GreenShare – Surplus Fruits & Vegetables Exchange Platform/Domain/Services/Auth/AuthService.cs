using Domain.Exceptions;
using Domain.Helper;
using Domain.Interfaces.Auth;
using Domain.Interfaces.Email;
using Domain.Interfaces.Tok;
using Domain.Models;
using Domain.Services.Email;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enum;

namespace Domain.Services.Auth
{
    public class AuthService:IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly ITokenService _token;
        private readonly IEmailService _email;

        public AuthService(IAuthRepository repo, ITokenService token, IEmailService email)
        {
            _repo = repo;
            _token = token;
            _email = email;
        }

        public async Task RegisterAsync(User user)
        {
            if (await _repo.EmailExistsAsync(user.Email))
                throw new BusinessException("Email already exists");

            user.EmailConfirmationToken = Guid.NewGuid().ToString();
            user.EmailConfirmed = false;

            await _repo.AddUserAsync(user);
            await _repo.SaveAsync();

            var link = $"https://localhost:5001/api/auth/confirm-email?token={user.EmailConfirmationToken}";

            await _email.SendEmailAsync(new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Confirm your email",
                Body = $"Click <a href='{link}'>here</a> to confirm email"
            });
        }

        public async Task ConfirmEmailAsync(string token)
        {
            var user = await _repo.GetByConfirmationTokenAsync(token);
            if (user == null) throw new BusinessException("Invalid or expired confirmation link");

            user.EmailConfirmed = true;
            user.EmailConfirmationToken = null;
            await _repo.SaveAsync();
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _repo.GetByEmailAsync(email);
            if (user == null) throw new BusinessException("Invalid email or password");

            if (!user.EmailConfirmed)
                throw new BusinessException("Email not confirmed");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new BusinessException("Invalid email or password");

            if (!user.IsActive)
                throw new BusinessException("Account blocked");


            if (user.Role != UserRole.Admin)
            {
                await _email.SendEmailAsync(new MailRequest
                {
                    ToEmail = user.Email,
                    Subject = "Login Successful",
                    Body = $"Hello {user.Name},<br/><br/>You have successfully logged in to GreenShare."
                });
            }


            return _token.CreateToken(user);
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _repo.GetByIdAsync(userId);
            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                throw new BusinessException("Current password incorrect");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _repo.SaveAsync();
        }
    }
}
