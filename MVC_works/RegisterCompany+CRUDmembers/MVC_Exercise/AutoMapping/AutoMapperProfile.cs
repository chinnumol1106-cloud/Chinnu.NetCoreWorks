using AutoMapper;
using MVC_Exercise.Models.DTOs;
using MVC_Exercise.Models.Entities;

namespace MVC_Exercise.AutoMapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CompanyDto,Company>().ReverseMap();
            CreateMap<MemberDto,Member>().ReverseMap();
        }
    }
}
