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
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AuthService(
            IAuthRepository repo,
            ITokenService tokenService,
            IEmailService emailService)
        {
            _repo = repo;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task RegisterAsync(string name, string email, string password, UserRole role,CompanyType? companyType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessException("Name is required");

            if (string.IsNullOrWhiteSpace(email))
                throw new BusinessException("Email is required");

            if (!email.Contains("@"))
                throw new BusinessException("Invalid email format");

            if (string.IsNullOrWhiteSpace(password))
                throw new BusinessException("Password is required");

            if (password.Length < 6)
                throw new BusinessException("Password must be at least 6 characters");
            if(role==UserRole.Admin)
                throw new BusinessException("admin cannot register");

            if (!System.Enum.IsDefined(typeof(UserRole), role))
                throw new BusinessException("Invalid role");

            if (companyType.HasValue &&
                !System.Enum.IsDefined(typeof(CompanyType), companyType.Value))
                throw new BusinessException("Invalid company type");

            if (role == UserRole.Company && companyType == null)
                throw new BusinessException("Company type is required for company role");

            if (role != UserRole.Company && companyType != null)
                throw new BusinessException("Only company role can have company type");






            if (await _repo.EmailExistsAsync(email))
                throw new BusinessException("Email already exists");
            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password), 
                Role = role,
                EmailConfirmed = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                EmailConfirmationToken = Guid.NewGuid().ToString()
            };

            await _repo.AddUserAsync(user);


            if (role == UserRole.Company)
            {
                if (companyType == null)
                    throw new BusinessException("Company type is required");

                var companyentity = new CompanyEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    CompanyType = companyType.Value
                };

                await _repo.AddCompanyAsync(companyentity);
            }




            await _repo.SaveAsync();

            var link = $"https://localhost:7167/api/auth/confirm-email?token={user.EmailConfirmationToken}";

            await _emailService.SendEmailAsync(new MailRequest
            {
                ToEmail = email,
                Subject = "Confirm your email",
                Body = $"Click <a href='{link}'>here</a> to confirm email."
            });
        }

        public async Task ConfirmEmailAsync(string token)
        {
            var user = await _repo.GetByConfirmationTokenAsync(token);
            if (user == null)
                throw new BusinessException("Invalid confirmation token");

            user.EmailConfirmed = true;
            user.EmailConfirmationToken = null;

            await _repo.SaveAsync();
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _repo.GetByEmailAsync(email);
            if (user == null)
                throw new BusinessException("Invalid credentials");

            if (!user.EmailConfirmed)
                throw new BusinessException("Email not confirmed");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new BusinessException("Invalid credentials");

            if (string.IsNullOrWhiteSpace(email))
                throw new BusinessException("Email is required");

            if (string.IsNullOrWhiteSpace(password))
                throw new BusinessException("Password is required");

            if (!user.IsActive)
                throw new BusinessException("Account blocked");

            await _emailService.SendEmailAsync(new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Login successful",
                Body = "You have successfully logged in."
            });

            return _tokenService.CreateToken(user);
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, string confirmPassword)
        {
            var user = await _repo.GetByIdAsync(userId);
            if (user == null)
                throw new BusinessException("User not found");

            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                throw new BusinessException("Current password incorrect");

            if (newPassword != confirmPassword)
                throw new BusinessException("Passwords do not match");
            if (string.IsNullOrWhiteSpace(currentPassword))
                throw new BusinessException("Current password is required");

            if (string.IsNullOrWhiteSpace(newPassword))
                throw new BusinessException("New password is required");

            if (newPassword.Length < 6)
                throw new BusinessException("New password must be at least 6 characters");

            if (newPassword == currentPassword)
                throw new BusinessException("New password must be different from old password");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _repo.SaveAsync();

            await _emailService.SendEmailAsync(new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Password changed",
                Body = "Your password has been changed successfully."
            });
        }
    }
}
