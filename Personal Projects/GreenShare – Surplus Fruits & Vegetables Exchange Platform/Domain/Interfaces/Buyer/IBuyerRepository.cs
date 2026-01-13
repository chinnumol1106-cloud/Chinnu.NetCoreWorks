using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Buyer
{
    public interface IBuyerRepository
    {
        Task AddInterestAsync(Interest interest);
        Task<List<Interest>> GetBuyerInterestsAsync(Guid buyerId);

        Task<List<Item>> FilterItemsAsync(string? itemName, string? city);

        Task<bool> InterestExistsAsync(Guid buyerId, Guid itemId);

        Task<List<Item>> GetAllItemsAsync();

        Task<Item?> GetItemByIdAsync(Guid itemId);
       

        Task SaveAsync();
    }
}
