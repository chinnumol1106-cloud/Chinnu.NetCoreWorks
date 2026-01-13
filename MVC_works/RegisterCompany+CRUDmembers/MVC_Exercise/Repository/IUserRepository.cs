
using MVC_Exercise.Models.Entities;

namespace MVC_Exercise.Repository
{
    public interface IUserRepository
    {
       Member GetUserByEmail(string email);
        void AddMember(Member addMember);
        Member GetMemberByDetails(Guid companyId,string firstName, string designation);
        void Delete(Member member);

        List<Member> GetMembersByCompanyid(Guid companyid, string search);
        Member GetMemberById(Guid id);

        //List<Member> GetMembersBysearch(string search);
    }
}
