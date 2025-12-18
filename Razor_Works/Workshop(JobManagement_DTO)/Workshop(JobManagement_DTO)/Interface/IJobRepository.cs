using Workshop_JobManagement_DTO_.Model;
using Workshop_JobManagement_DTO_.Service;

namespace Workshop_JobManagement_DTO_.Interface
{
    public interface IJobRepository
    {

        public Task<List<Job>> GetAlljobsAsync();

        public Task AddJobAsync(Job job);

        public Task<Job> GetJobByIdAsync(int id);

        public Task DeleteJobAsync(int id);

     
       public Task UpdateJobAsync(int id, Job job);
    }
}
