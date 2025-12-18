using AutoMapper;
using RAZOR_LOGIN_DTO_CFA_.DTOs;
using RAZOR_LOGIN_DTO_CFA_.Models;

namespace RAZOR_LOGIN_DTO_CFA_.Profiles
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            CreateMap<RegisterDto, User>();
        }
    }
}
