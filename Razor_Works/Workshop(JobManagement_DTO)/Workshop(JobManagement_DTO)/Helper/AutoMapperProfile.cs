using AutoMapper;
using Workshop_JobManagement_DTO_.Dto;
using Workshop_JobManagement_DTO_.Model;

namespace Workshop_JobManagement_DTO_.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Job, JobDto>().ReverseMap();
        }
    }
}
