using Blazor_JobProvider.Helpers;
using Blazor_JobProvider.Interface;
using Blazor_JobProvider.Model;
using Blazor_JobProvider.Repository;
using Blazor_JobProvider.Service;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Blazor_JobProvider.Extension
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices
           (this IServiceCollection services, IConfiguration config)
        {
            //services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddScoped<ProtectedSessionStorage>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IJobProviderRepository, JobProviderRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobService, JobService>();
            return services;
        }
    }

    
}
