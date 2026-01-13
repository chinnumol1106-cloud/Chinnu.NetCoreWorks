using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Admin
{
    public interface IAdminRepository
    {

        Task AddItemTypeAsync(ItemType itemType);
        Task AddItemNameAsync(ItemName itemName);

        Task<List<ItemType>> GetItemTypesAsync();
        Task<List<ItemName>> GetItemNamesByTypeAsync(Guid itemTypeId);

        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid userId);

        Task<List<Interest>> GetAllInterestsAsync();

        Task<List<Item>> GetSoldItemsAsync();
        Task SaveAsync();

    }
}
