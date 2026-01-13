using Domain.Enum;
using Domain.Exceptions;
using Domain.Interfaces.Buyer;
using Domain.Models;

using Domain.Repositories.Buyer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Services.Buyer
{
    public class BuyerService:IBuyerService
    {
        private readonly IBuyerRepository _repo;

        public BuyerService(IBuyerRepository repo)
        {
            _repo = repo;
        }

        public async Task SendInterestAsync(Guid buyerId, Guid itemId)
        {


            // 1️⃣ Get item
            var item = await _repo.GetItemByIdAsync(itemId)
                ?? throw new BusinessException("Item not found");

            // 2️⃣ Block if sold or removed
            if (item.Status == ItemStatus.Sold)
                throw new BusinessException("This item is already sold");

            if (item.Status == ItemStatus.Removed)
                throw new BusinessException("This item is no longer available");

            // 3️⃣ Prevent duplicate interest
            bool alreadySent = await _repo.InterestExistsAsync(buyerId, itemId);
            if (alreadySent)
                throw new BusinessException("You have already sent interest for this item");

            // 4️⃣ Create interest
            var interest = new Interest
            {
                BuyerId = buyerId,
                ItemId = itemId,
                Status = InterestStatus.Pending
            };

            await _repo.AddInterestAsync(interest);
            await _repo.SaveAsync();
        }

        public async Task<List<Interest>> GetMyInterestsAsync(Guid buyerId)
        {
            return await _repo.GetBuyerInterestsAsync(buyerId);
        }

        public async Task<List<Item>> FilterItemsAsync(string? itemName, string? city)
        {
            return await _repo.FilterItemsAsync(itemName, city);
        }


        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _repo.GetAllItemsAsync();
        }





    }
}
