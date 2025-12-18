using Microsoft.EntityFrameworkCore;

namespace Employee_Blazor_machineTest.Model
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
