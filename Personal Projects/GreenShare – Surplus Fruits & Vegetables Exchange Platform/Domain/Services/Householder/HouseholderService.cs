using Domain.Enum;
using Domain.Exceptions;
using Domain.Interfaces.Householder;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Householder
{
    public class HouseholderService : IHouseholderService
    {


        private readonly IHouseholderRepository _repo;

        public HouseholderService(IHouseholderRepository repo)
        {
            _repo = repo;
        }

        public async Task AddItemAsync(Item item)
        {
            await _repo.AddItemAsync(item);
            await _repo.SaveAsync();
        }

        public async Task<List<Item>> GetMyItemsAsync(Guid sellerId)
           {
            return await _repo.GetMyActiveItemsAsync(sellerId);

        }

        public async Task AcceptInterestAsync(Guid sellerId, Guid interestId)
        {
            var interest = await _repo.GetInterestByIdAsync(interestId)
         ?? throw new BusinessException("Interest not found");

            var item = await _repo.GetItemByIdAsync(interest.ItemId)
                ?? throw new BusinessException("Item not found");

            // 🔐 OWNERSHIP CHECK (REQUIRED)
            if (item.SellerId != sellerId)
                throw new BusinessException("You are not allowed");

           // ✅ Accept selected interest
    interest.Status = InterestStatus.Accepted;

            // ✅ Reject all others
            foreach (var other in item.Interests)
            {
                if (other.Id != interestId)
                    other.Status = InterestStatus.Rejected;
            }

            // ✅ Item is SOLD
            item.Status = ItemStatus.Sold;

            await _repo.SaveAsync();
        }

        public async Task RejectInterestAsync(Guid sellerId, Guid interestId)
        {
            var interest = await _repo.GetInterestByIdAsync(interestId)
        ?? throw new BusinessException("Interest not found");

            var item = await _repo.GetItemByIdAsync(interest.ItemId)
                ?? throw new BusinessException("Item not found");

            // 🔐 OWNERSHIP CHECK
            if (item.SellerId != sellerId)
                throw new BusinessException("You are not allowed");

            interest.Status = InterestStatus.Rejected;

            await _repo.SaveAsync();
        }

        public async Task MarkItemAsSoldAsync(Guid sellerId, Guid itemId)
        {
            var item = await _repo.GetItemByIdAsync(itemId)
        ?? throw new BusinessException("Item not found");

            // 🔐 OWNERSHIP CHECK
            if (item.SellerId != sellerId)
                throw new BusinessException("You are not allowed");

            item.Status = ItemStatus.Sold;
            await _repo.SaveAsync();

        }
    }
}
