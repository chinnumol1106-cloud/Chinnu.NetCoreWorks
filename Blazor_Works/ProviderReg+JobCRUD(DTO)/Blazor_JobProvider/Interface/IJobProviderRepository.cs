using Blazor_JobProvider.Model;

namespace Blazor_JobProvider.Interface
{
    public interface IJobProviderRepository
    {

       Task<JobProvider> GetByEmailAsync(string email);
        Task AddAsync(JobProvider AddProvider);
    }
}

