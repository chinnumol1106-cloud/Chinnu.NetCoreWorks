using Blazor_JobProvider.Interface;
using Blazor_JobProvider.Model;
using Microsoft.EntityFrameworkCore;

namespace Blazor_JobProvider.Repository
{
    public class JobRepository:IJobRepository
    {
        private readonly AppDbContext _context;

        public JobRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Job>> GetJobByProviderIdAsync(int id)
        {
            return await _context.Jobs.Where(j=>j.JobProviderId== id).ToListAsync();
        }

        public async Task AddAsync(Job job)
        {
            _context.Jobs.Add(job);
           await  _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }

        public async Task<Job> GetByIdAsync(int jobId)
        {
            return await _context.Jobs.FindAsync(jobId);
        }

        public async Task DeleteAsync(int jobId)
        {
            var job = await _context.Jobs.FindAsync(jobId);
            if (job != null)
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Job>> GetJobBySearchTextAsync(string searchtext)
        {
            return await _context.Jobs.Where(j=>j.Title.Contains(searchtext)).ToListAsync();
        }
    }
}
