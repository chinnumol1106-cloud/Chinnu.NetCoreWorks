using Domain.Exceptions;
using Domain.Interfaces.Profile;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Profile
{
    public class ProfileService:IProfileService
    {
        private readonly IProfileRepository _repo;

        public ProfileService(IProfileRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserProfile> CreateAsync(Guid userId, UserProfile profile)
        {
            var existing = await _repo.GetByUserIdAsync(userId);
            if (existing != null)
                throw new BusinessException("Profile already exists");

            profile.UserId = userId;
            await _repo.AddAsync(profile);
            await _repo.SaveAsync();
            return profile;
        }

        public async Task<UserProfile> GetMyProfileAsync(Guid userId)
        {
            var profile = await _repo.GetByUserIdAsync(userId);
            if (profile == null)
                throw new BusinessException("Profile not found");

            return profile;
        }

        public async Task<UserProfile> UpdateAsync(Guid userId, UserProfile profile)
        {
            var existing = await _repo.GetByUserIdAsync(userId);
            if (existing == null)
                throw new BusinessException("Profile not found");

            existing.Address = profile.Address;
            existing.City = profile.City;
            existing.County = profile.County;
            existing.ZipCode = profile.ZipCode;
            existing.PhoneNumber = profile.PhoneNumber;

            await _repo.UpdateAsync(existing);
            await _repo.SaveAsync();
            return existing;
        }

        public async Task DeleteAsync(Guid userId)
        {
            var profile = await _repo.GetByUserIdAsync(userId);
            if (profile == null)
                throw new BusinessException("Profile not found");

            await _repo.DeleteAsync(profile);
            await _repo.SaveAsync();
        }
    }
}
