using MVC_Exercise.Models.DTOs;

namespace MVC_Exercise.Services
{
    public interface IUserService
    {
       bool AddMember(MemberDto newMember);
        bool DeleteMemberByDetails(Guid companyId, string firstName, string designation);
        List<MemberDto> GetMembersByCompanyId(Guid companyid,string search);
        MemberDto GetMemberById(Guid id);

        //List<MemberDto> GetMembersBysearch(string search);

    }
}
