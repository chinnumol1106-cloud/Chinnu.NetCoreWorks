using Domain.Data;
using Domain.Interfaces.Admin;
using Domain.Interfaces.Auth;
using Domain.Interfaces.Buyer;
using Domain.Interfaces.Company;
using Domain.Interfaces.Email;
using Domain.Interfaces.Householder;
using Domain.Interfaces.Pay;

//using Domain.Interfaces.Pay;
using Domain.Interfaces.Profile;
using Domain.Interfaces.Tok;
using Domain.Repositories.Admin;
using Domain.Repositories.Auth;
using Domain.Repositories.Buyer;
using Domain.Repositories.Company;
using Domain.Repositories.Householder;
using Domain.Repositories.Pay;

//using Domain.Repositories.Pay;
using Domain.Repositories.Profile;
using Domain.Services.Admin;
using Domain.Services.Auth;
using Domain.Services.Buyer;
using Domain.Services.Company;
using Domain.Services.Email;
using Domain.Services.Householder;
//using Domain.Services.Pay;
using Domain.Services.Profile;
using Domain.Services.Tok;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extension
{
    public static class ApplicationServiceExtensions
    {


        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IAppleOwnerService, AppleOwnerService>();
            services.AddScoped<IAppleOwnerRepository, AppleOwnerRepository>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
           
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();


           



            return services;
        }

    }
}
