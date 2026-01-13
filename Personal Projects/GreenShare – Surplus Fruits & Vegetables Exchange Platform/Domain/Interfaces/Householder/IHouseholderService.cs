using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Householder
{
    public interface IHouseholderService
    {
        Task AddItemAsync(Item item);

        Task<List<Item>> GetMyItemsAsync(Guid sellerId);

        Task AcceptInterestAsync(Guid sellerId, Guid interestId);

        Task RejectInterestAsync(Guid sellerId, Guid interestId);

        Task MarkItemAsSoldAsync(Guid sellerId, Guid itemId);
    }
}
