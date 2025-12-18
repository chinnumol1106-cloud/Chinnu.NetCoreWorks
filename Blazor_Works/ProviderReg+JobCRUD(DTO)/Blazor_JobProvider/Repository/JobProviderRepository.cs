using Blazor_JobProvider.Interface;
using Blazor_JobProvider.Model;
using Microsoft.EntityFrameworkCore;

namespace Blazor_JobProvider.Repository
{
    public class JobProviderRepository:IJobProviderRepository
    {

        private readonly AppDbContext _context;

        public JobProviderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<JobProvider> GetByEmailAsync(string email)
        {
            return await _context.JobProviders.FirstOrDefaultAsync(jp => jp.Email == email);

        }

        public async Task AddAsync(JobProvider AddProvider)
        {
            _context.JobProviders.Add(AddProvider);
            await _context.SaveChangesAsync();
        }
    }
}
