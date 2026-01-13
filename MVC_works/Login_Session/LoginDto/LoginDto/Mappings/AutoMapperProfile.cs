using AutoMapper;
using LoginDto.DTOs;
using LoginDto.Models;

namespace LoginDto.Mappings
{
    public class AutoMapperProfile:Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<UserDTO,User>().ReverseMap();
           
        }
    }
}
