using Machine_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Machine_Test.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
