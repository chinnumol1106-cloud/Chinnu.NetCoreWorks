using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Buyer
{
    public interface IBuyerService
    {
        Task SendInterestAsync(Guid buyerId, Guid itemId);
        Task<List<Interest>> GetMyInterestsAsync(Guid buyerId);
        Task<List<Item>> FilterItemsAsync(string? itemName, string? city);
        //Task<List<Item>> GetAllItemsAsync();

        Task<List<Item>> GetAllItemsAsync();
    }
}
