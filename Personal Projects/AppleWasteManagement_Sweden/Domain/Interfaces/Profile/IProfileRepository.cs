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
        Task<UserProfile> GetByUserIdAsync(Guid userId);
        Task AddAsync(UserProfile profile);
        Task UpdateAsync(UserProfile profile);
        Task DeleteAsync(UserProfile profile);
        Task SaveAsync();
    }
}
