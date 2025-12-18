using AutoMapper;
using Blazor_JobProvider.Dto;
using Blazor_JobProvider.Model;

namespace Blazor_JobProvider.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<JobProvider, JobProviderDto>().ReverseMap();
            CreateMap<Job, JobDto>().ReverseMap();
        }
    }
}
