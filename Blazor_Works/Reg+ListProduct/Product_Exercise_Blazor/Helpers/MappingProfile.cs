using AutoMapper;
using Product_Exercise_Blazor.Dto;
using Product_Exercise_Blazor.Model;

namespace Product_Exercise_Blazor.Helpers
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<User,RegisterDto>().ReverseMap();
        }
    }
}
