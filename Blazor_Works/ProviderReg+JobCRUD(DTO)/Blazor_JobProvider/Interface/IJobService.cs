using Blazor_JobProvider.Dto;
using Blazor_JobProvider.Model;

namespace Blazor_JobProvider.Interface
{
    public interface IJobService
    {
       Task<List<JobDto>> GetJobByProviderIdAsync(int id);
        Task<bool> AddJobAsync(JobDto jobDto, int providerId);

        Task<bool> UpdateJobAsync(JobDto jobDto);
        Task<bool> DeleteJobAsync(int jobId);

        Task<List<JobDto>> GetJobBySearchTextAsync(string searchtext);

    }
}
