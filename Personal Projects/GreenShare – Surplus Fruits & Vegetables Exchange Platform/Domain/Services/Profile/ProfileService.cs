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

        public async Task CreateProfileAsync(UserProfile profile)
        {
            if (await _repo.ProfileExistsAsync(profile.UserId))
                throw new BusinessException("Profile already exists");

            await _repo.AddProfileAsync(profile);
            await _repo.SaveAsync();
        }

        public async Task<UserProfile> GetMyProfileAsync(Guid userId)
        {
            var profile = await _repo.GetProfileByUserIdAsync(userId);

            if (profile == null)
                throw new BusinessException("Profile not found");

            return profile;
        }

        public async Task UpdateProfileAsync(Guid userId, Action<UserProfile> updateAction)
        {
            var profile = await _repo.GetProfileByUserIdAsync(userId)
                ?? throw new BusinessException("Profile not found");

            updateAction(profile);
            await _repo.SaveAsync();
        }

        public async Task DeleteProfileAsync(Guid userId)
        {
            var profile = await _repo.GetProfileByUserIdAsync(userId)
                ?? throw new BusinessException("Profile not found");

            await _repo.DeleteProfileAsync(profile);
            await _repo.SaveAsync();
        }
    }
}
