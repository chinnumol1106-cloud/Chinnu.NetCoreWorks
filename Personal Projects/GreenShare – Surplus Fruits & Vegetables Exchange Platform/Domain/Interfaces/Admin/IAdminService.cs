using Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Admin
{
    public interface IAdminService
    {

        Task AddItemTypeAsync(ItemType itemType);
        Task AddItemNameAsync(ItemName itemName);

        Task<List<ItemType>> GetItemTypesAsync();
        Task<List<ItemName>> GetItemNamesByTypeAsync(Guid itemTypeId);

        Task UpdateUserStatusAsync(Guid userId, bool isActive);
        Task<List<User>> GetAllUsersAsync();

        Task<List<Interest>> GetAllInterestsAsync();

        Task<int> RemoveAllSoldItemsAsync();

    }
}
