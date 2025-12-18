using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Product_Exercise_Blazor.Helpers;
using Product_Exercise_Blazor.Model;
using Product_Exercise_Blazor.Repository;
using Product_Exercise_Blazor.Service;

namespace Product_Exercise_Blazor.Extension
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices
           (this IServiceCollection services, IConfiguration config)
        {

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddScoped<ProtectedSessionStorage>();

            services.AddDbContext<ProductAppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
