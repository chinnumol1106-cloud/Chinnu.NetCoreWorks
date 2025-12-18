using AutoMapper;
using Product_Exercise_Blazor.Dto;
using Product_Exercise_Blazor.Model;
using Product_Exercise_Blazor.Repository;

namespace Product_Exercise_Blazor.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public void AddProduct(ProductDto product, int userid)
        {
            var newproduct=_mapper.Map<Product>(product);
            newproduct.UserId = userid;
            _productRepository.Addproduct(newproduct);
        }

        public async Task<List<ProductDto>> GetProductByUserId(int id)
        {
            var productList= await _productRepository.GetProductByUserId(id);
            var newproductlist = _mapper.Map<List<ProductDto>>(productList);
            return newproductlist;
           
        }
        public async Task<List<string>> GetCategoriesByUserId(int userid)
        {
            return await _productRepository.GetCategoriesByUserId(userid);
        }


        public async Task<List<ProductDto>> GetProductByCategory(int userId,string category)
        {
            var products = await _productRepository.GetProductByCategory(userId,category);
            return _mapper.Map<List<ProductDto>>(products);
        }

    }
}
