using Microsoft.EntityFrameworkCore;
using Workshop_JobManagement_DTO_.Helper;
using Workshop_JobManagement_DTO_.Interface;
using Workshop_JobManagement_DTO_.Model;
using Workshop_JobManagement_DTO_.Service;
using Workshop_JobManagement_DTO_.Repository;


namespace Workshop_JobManagement_DTO_.Extension
{
    public static class ApplicationServiceExtension
    {

        public static IServiceCollection AddApplicationServices
           (this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // Add Services
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobService, JobService>();

            // Add AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }
    }
}
