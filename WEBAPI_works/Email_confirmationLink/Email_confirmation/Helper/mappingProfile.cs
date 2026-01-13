using AutoMapper;
using Email_confirmation.Models;

namespace Email_confirmation.Helper
{
    public class mappingProfile:Profile
    {
        public mappingProfile()
        {
            CreateMap<User,RegisterDto>().ReverseMap();
        }
    }
}
