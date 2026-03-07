using Domain.Enum;
using Domain.Models;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Interfaces.Auth
{
    public interface IAuthService
    {

        Task RegisterAsync(string name, string email, string password, UserRole role,CompanyType? companyType);
        Task ConfirmEmailAsync(string token);
        Task<string> LoginAsync(string email, string password);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, string confirmPassword);
    }
}
