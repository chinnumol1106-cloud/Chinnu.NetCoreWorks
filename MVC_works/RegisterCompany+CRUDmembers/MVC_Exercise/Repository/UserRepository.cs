using MVC_Exercise.Data;
using MVC_Exercise.Models.Entities;

namespace MVC_Exercise.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public Member GetUserByEmail(string email)
        {
          return _context.Members.FirstOrDefault(u=>u.Email==email);
        }

        public void AddMember(Member addMember)
        {
            _context.Members.Add(addMember);
            _context.SaveChanges();
        }

        public Member GetMemberByDetails(Guid companyId, string firstName, string designation)
        {
            return _context.Members.FirstOrDefault(m => m.CompanyId == companyId && m.FirstName.ToLower() == firstName.ToLower()&& m.Designation.ToLower()== designation.ToLower());
        }

        public void Delete(Member member)
        {
            _context.Members.Remove(member);
            _context.SaveChanges();
        }

        public List<Member> GetMembersByCompanyid(Guid companyid, string search)
        {
            if(string.IsNullOrEmpty(search))
            {
                return _context.Members.Where(m => m.CompanyId == companyid).ToList();
            }
             return _context.Members.Where(b => b.FirstName.Contains(search)).ToList();
            

        }

        public Member GetMemberById(Guid id)
        {
            return _context.Members.FirstOrDefault(m => m.MemberId == id);
        }
        //public List<Member> GetMembersBysearch(string search)
        //{
        //    var allmembers = _context.Members.Where(m => m.FirstName.Contains(search)).ToList();
        //    return allmembers;
        //}
    }
}
