using Microsoft.EntityFrameworkCore;
using Product_Exercise_Blazor.Model;

namespace Product_Exercise_Blazor.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductAppDbContext _context;

        public ProductRepository(ProductAppDbContext context)
        {
            _context = context;
        }

        public  void Addproduct(Product newproduct)
        {
            _context.Products.Add(newproduct);
            _context.SaveChanges();
        }

        public async Task<List<Product>> GetProductByUserId(int id)
        {
            return await _context.Products.Where(p=>p.UserId == id).ToListAsync();
        }


        public async Task<List<string>> GetCategoriesByUserId(int userId)
        {
           
            return await _context.Products .Where(p => p.UserId == userId).Select(p => p.Category).Distinct().ToListAsync();



        }



        public async Task<List<Product>> GetProductByCategory(int userId,string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return await _context.Products.Where(p => p.UserId == userId) .ToListAsync();
            }

            else
            {

              return await _context.Products.Where(p => p.UserId == userId && p.Category == category).ToListAsync();
            }


        }

    }
}
