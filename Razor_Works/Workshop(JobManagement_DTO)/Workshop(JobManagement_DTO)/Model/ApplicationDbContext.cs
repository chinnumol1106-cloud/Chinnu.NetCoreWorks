using Microsoft.EntityFrameworkCore;


namespace Workshop_JobManagement_DTO_.Model
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<Job> Jobs { get; set; }
    }
}
