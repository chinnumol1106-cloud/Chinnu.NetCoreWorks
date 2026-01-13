using Microsoft.EntityFrameworkCore;
using System.Data;

namespace JWT_Activity2.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }  
      
    }
}
