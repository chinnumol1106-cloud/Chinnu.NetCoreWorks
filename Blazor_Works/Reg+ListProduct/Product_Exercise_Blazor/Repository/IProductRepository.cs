using Product_Exercise_Blazor.Model;

namespace Product_Exercise_Blazor.Repository
{
    public interface IProductRepository
    {
        void Addproduct(Product newproduct);
        Task<List<Product>> GetProductByUserId(int id);
        Task<List<string>> GetCategoriesByUserId(int userId);
        Task<List<Product>> GetProductByCategory(int userId,string category);

    }
}
