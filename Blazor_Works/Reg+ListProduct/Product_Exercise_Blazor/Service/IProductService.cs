using Product_Exercise_Blazor.Dto;
using Product_Exercise_Blazor.Model;

namespace Product_Exercise_Blazor.Service
{
    public interface IProductService
    {
        void AddProduct(ProductDto product, int userid);
        Task<List<ProductDto>> GetProductByUserId(int id);
        Task<List<string>> GetCategoriesByUserId(int userid);
        Task<List<ProductDto>> GetProductByCategory(int userId,string category);
    }
}
