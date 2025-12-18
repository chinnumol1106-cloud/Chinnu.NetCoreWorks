using Microsoft.EntityFrameworkCore;

namespace Blazor_JobProvider.Model
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Job> Jobs   { get;set; }
        public DbSet<JobProvider> JobProviders { get;set; }
    }
}
