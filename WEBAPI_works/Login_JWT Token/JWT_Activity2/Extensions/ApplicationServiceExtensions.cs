using JWT_Activity2.Interface;
using JWT_Activity2.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace JWT_Activity2.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
            );



            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AppDbContext>();
            services.AddScoped<ITokenInterface, Tok>();
           

            return services;
        }
    }
}

