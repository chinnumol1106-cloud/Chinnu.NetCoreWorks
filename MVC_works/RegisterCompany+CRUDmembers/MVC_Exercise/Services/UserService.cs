using AutoMapper;

using MVC_Exercise.Models.DTOs;
using MVC_Exercise.Models.Entities;
using MVC_Exercise.Repository;

namespace MVC_Exercise.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public bool AddMember(MemberDto newMember)
        {
            var existmember=_userRepository.GetUserByEmail(newMember.Email);
            if(existmember!=null)
            {
                return false;
            }
            var addMember = _mapper.Map<Member>(newMember);
            _userRepository.AddMember(addMember);
            return true;    
        }

        public bool DeleteMemberByDetails(Guid companyId, string firstName, string designation)
        {
            var member=_userRepository.GetMemberByDetails(companyId, firstName, designation);
            if(member!=null)
            {
                _userRepository.Delete(member);
                return true;
            }
            return false;
        }

        public List<MemberDto> GetMembersByCompanyId(Guid companyid, string search)
        {
            var memberslist=_userRepository.GetMembersByCompanyid(companyid,search);
            if(memberslist!=null)
            {
                var listmember=_mapper.Map<List<MemberDto>>(memberslist);
                return listmember;
            }
            return null;
          
        }

        public MemberDto GetMemberById(Guid id)
        {
            var member = _userRepository.GetMemberById(id);
            var viewmember=_mapper.Map<MemberDto>(member);
            return viewmember;
        }
        //public List<MemberDto> GetMembersBysearch(string search)
        //{
        //    var memberslist = _userRepository.GetMembersBysearch(search);
        //    if (memberslist != null)
        //    {
        //        var listmember = _mapper.Map<List<MemberDto>>(memberslist);
        //        return listmember;
        //    }
        //    return null;

        //}
    }
}
