using BlazorActivity_ServiceRepo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorActivity_ServiceRepo.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
