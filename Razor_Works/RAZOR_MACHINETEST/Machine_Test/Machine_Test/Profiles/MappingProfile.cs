using Machine_Test.Dto;
using Machine_Test.Models;
using AutoMapper;

namespace Machine_Test.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, User>();
        }
    }
    
}
