using Domain.Data;
using Domain.Interfaces.Profile;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Profile
{
    public class ProfileRepository:IProfileRepository
    {

        private readonly AppDbContext _context;

        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task AddAsync(UserProfile profile)
        {
            await _context.UserProfiles.AddAsync(profile);
        }

        public async Task UpdateAsync(UserProfile profile)
        {
            _context.UserProfiles.Update(profile);
        }

        public async Task DeleteAsync(UserProfile profile)
        {
            _context.UserProfiles.Remove(profile);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
