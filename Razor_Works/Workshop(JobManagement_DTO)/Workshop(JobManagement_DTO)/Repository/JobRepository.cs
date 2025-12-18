using Microsoft.EntityFrameworkCore;
using Workshop_JobManagement_DTO_.Interface;
using Workshop_JobManagement_DTO_.Model;
using Workshop_JobManagement_DTO_.Service;

namespace Workshop_JobManagement_DTO_.Repository
{
    public class JobRepository:IJobRepository
    {

        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Job>> GetAlljobsAsync()
        {
            var jobs = await _context.Jobs.ToListAsync();
            return jobs;
        }

        public async Task<Job> GetJobByIdAsync(int id)
        {
            var job= await _context.Jobs.FindAsync(id);
            return job;

        }


        public async Task AddJobAsync(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteJobAsync(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if(job != null)
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateJobAsync(int id,Job job)
        {
            var existingjob = await _context.Jobs.FindAsync(id);

            if(existingjob == null)
            
                return;
            

            _context.Entry(existingjob).State=EntityState.Detached;


            var updatedjob = job;
            updatedjob.Id = id;

            _context.Jobs.Add(updatedjob);
            _context.Entry(updatedjob).State=EntityState.Modified;
            await _context.SaveChangesAsync();


        }
    }
}
