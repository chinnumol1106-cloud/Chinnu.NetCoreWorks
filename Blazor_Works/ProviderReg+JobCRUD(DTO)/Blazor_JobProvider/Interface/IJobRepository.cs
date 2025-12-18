using Blazor_JobProvider.Dto;
using Blazor_JobProvider.Model;

namespace Blazor_JobProvider.Interface
{
    public interface IJobRepository
    {
        Task<List<Job>> GetJobByProviderIdAsync(int id);
        Task AddAsync(Job job);
        Task UpdateAsync(Job job);
        Task<Job> GetByIdAsync(int jobId);
        Task DeleteAsync(int jobId);

        Task<List<Job>> GetJobBySearchTextAsync(string searchtext);

    }
}
