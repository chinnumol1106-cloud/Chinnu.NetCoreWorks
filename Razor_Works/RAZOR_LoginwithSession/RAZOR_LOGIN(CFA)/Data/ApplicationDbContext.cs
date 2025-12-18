using Microsoft.EntityFrameworkCore;
using RAZOR_LOGIN_CFA_.Models;

namespace RAZOR_LOGIN_CFA_.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

    }
}
