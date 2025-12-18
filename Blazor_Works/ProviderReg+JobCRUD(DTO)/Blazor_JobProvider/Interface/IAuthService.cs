using Blazor_JobProvider.Dto;
using Blazor_JobProvider.Model;

namespace Blazor_JobProvider.Interface
{
    public interface IAuthService
    {
     Task<bool> Registerr(JobProviderDto InputJobProvider,string  password);
   
     Task<bool> Loginprovider(string email, string password);
    }
}
