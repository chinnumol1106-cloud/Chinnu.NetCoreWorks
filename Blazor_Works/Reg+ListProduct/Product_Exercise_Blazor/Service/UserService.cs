using AutoMapper;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Product_Exercise_Blazor.Dto;
using Product_Exercise_Blazor.Model;
using Product_Exercise_Blazor.Repository;

namespace Product_Exercise_Blazor.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userrepository;
        private readonly IMapper _mapper;
        private readonly ProtectedSessionStorage _sessionStorage;

        public UserService(IUserRepository userrepository, IMapper mapper,ProtectedSessionStorage sessionStorage)
        {
            _userrepository = userrepository;
            _mapper=mapper;
            _sessionStorage = sessionStorage;
        }
        public async Task<bool> Registeruser(RegisterDto register, string password)
        {
            var existUser=await _userrepository.GetByEmailAsync(register.Email);

            if (existUser!=null)
            {
                return false;
            }

            var newuser=_mapper.Map<User>(register);
            newuser.PasswordHash=BCrypt.Net.BCrypt.HashPassword(password);
            await _userrepository.AddAsync(newuser);
            return true;
        }

        public async Task<bool> Login(string email, string password)
        {
             var loguser=await _userrepository.GetByEmailAsync(email);

            if(loguser!=null && BCrypt.Net.BCrypt.Verify(password,loguser.PasswordHash))
            {
                await _sessionStorage.SetAsync("UserId", loguser.Id);
                await _sessionStorage.SetAsync("UserName", loguser.Name);

                return true;
            }
            else
            {
              
                  
                return false;
                
                
            }
        }

    }
}
