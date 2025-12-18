using AutoMapper;
using Blazor_JobProvider.Dto;
using Blazor_JobProvider.Interface;
using Blazor_JobProvider.Model;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Blazor_JobProvider.Service
{
    public class AuthService:IAuthService
    {
        private readonly IJobProviderRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProtectedSessionStorage _sessionstorage;
     public AuthService(IJobProviderRepository repository, IMapper mapper, ProtectedSessionStorage sessionstorage)
        {
            _repository = repository;
            _mapper = mapper;
            _sessionstorage = sessionstorage;
        }

        public async Task<bool> Registerr(JobProviderDto InputJobProvider, string password)
        {
            var existprovider=await _repository.GetByEmailAsync(InputJobProvider.Email);

            if(existprovider!=null)
            {
                return false;
            }

            var newProvider=_mapper.Map<JobProvider>(InputJobProvider);
            newProvider.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

            await _repository.AddAsync(newProvider);
            return true;
        }

        public async Task<bool> Loginprovider(string email, string password)
        {
            var logExist= await _repository.GetByEmailAsync(email);

            if(logExist!=null && BCrypt.Net.BCrypt.Verify(password,logExist.PasswordHash))
            { 
             try
                {
                    await _sessionstorage.SetAsync("JobProviderId", logExist.Id);
                    await _sessionstorage.SetAsync("JobProviderName", logExist.Name);
                    Console.WriteLine("Session stored successfully");
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine(ex.Message);
                }
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
