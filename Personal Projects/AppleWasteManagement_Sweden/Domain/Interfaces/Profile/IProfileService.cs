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
        Task<UserProfile> CreateAsync(Guid userId, UserProfile profile);
        Task<UserProfile> GetMyProfileAsync(Guid userId);
        Task<UserProfile> UpdateAsync(Guid userId, UserProfile profile);
        Task DeleteAsync(Guid userId);
    }
}
