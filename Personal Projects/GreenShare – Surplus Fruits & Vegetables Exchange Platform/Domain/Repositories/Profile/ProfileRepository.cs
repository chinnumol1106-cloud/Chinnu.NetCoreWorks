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

        public async Task<bool> ProfileExistsAsync(Guid userId)
            => await _context.UserProfiles.AnyAsync(p => p.UserId == userId);

        public async Task AddProfileAsync(UserProfile profile)
            => await _context.UserProfiles.AddAsync(profile);

        public async Task<UserProfile?> GetProfileByUserIdAsync(Guid userId)
            => await _context.UserProfiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == userId);

        public async Task DeleteProfileAsync(UserProfile profile)
            => _context.UserProfiles.Remove(profile);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
