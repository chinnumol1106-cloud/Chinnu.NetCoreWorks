using CRUD_Studuent.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Studuent.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Student> Students { get; set; }
    }
}
