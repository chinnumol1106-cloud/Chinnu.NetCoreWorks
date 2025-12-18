using Exercise_RAZOR_Book_.Model;
using Microsoft.EntityFrameworkCore;

namespace Exercise_RAZOR_Book_.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
