using Workshop_JobManagement_DTO_.Dto;

namespace Workshop_JobManagement_DTO_.Interface
{
    public interface IJobService
    {
        public Task<List<JobDto>> GetAllJobsAsync();

        public Task AddJobAsync(JobDto jobdto);

        public Task<JobDto> GetJobByIdAsync(int id);

        public Task DeleteJobAsync(int id);

        public Task UpdateJobAsync(int id,JobDto jobdto);
    }
}
