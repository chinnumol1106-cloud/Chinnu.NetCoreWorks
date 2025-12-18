using Microsoft.EntityFrameworkCore;

namespace Product_Exercise_Blazor.Model
{
    public class ProductAppDbContext:DbContext
    {
        public ProductAppDbContext(DbContextOptions<ProductAppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
       

       
    }
}
