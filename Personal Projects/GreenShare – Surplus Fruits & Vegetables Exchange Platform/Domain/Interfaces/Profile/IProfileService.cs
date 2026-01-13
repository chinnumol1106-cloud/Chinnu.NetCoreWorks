using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Profile
{
    public interface IProfileService
    {
        Task CreateProfileAsync(UserProfile profile);
        Task<UserProfile> GetMyProfileAsync(Guid userId);
        Task UpdateProfileAsync(Guid userId, Action<UserProfile> updateAction);
        Task DeleteProfileAsync(Guid userId);
    }
}
