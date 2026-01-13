using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Householder
{
    public interface IHouseholderRepository
    {
        Task AddItemAsync(Item item);

        Task<List<Item>> GetMyActiveItemsAsync(Guid sellerId);
        Task<Interest?> GetInterestByIdAsync(Guid interestId);

        Task<Item?> GetItemByIdAsync(Guid itemId);

        Task SaveAsync();


    }
}
