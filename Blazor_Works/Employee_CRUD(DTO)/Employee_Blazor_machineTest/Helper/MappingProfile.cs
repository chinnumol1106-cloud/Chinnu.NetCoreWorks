using AutoMapper;
using Employee_Blazor_machineTest.Dto;
using Employee_Blazor_machineTest.Model;

namespace Employee_Blazor_machineTest.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDto,Employee>().ReverseMap();
        }
    }
}
