using Microsoft.EntityFrameworkCore;
using MVC_Exercise.Models.Entities;

namespace MVC_Exercise.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
