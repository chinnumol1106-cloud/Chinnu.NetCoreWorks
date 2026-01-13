using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Profile
{
    public interface IProfileRepository
    {
        Task<bool> ProfileExistsAsync(Guid userId);
        Task AddProfileAsync(UserProfile profile);

        Task<UserProfile?> GetProfileByUserIdAsync(Guid userId);

        Task DeleteProfileAsync(UserProfile profile);

        Task SaveAsync();
    }
}
