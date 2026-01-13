using Domain.Data;
using Domain.Enum;
using Domain.Interfaces.Buyer;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Repositories.Buyer
{
    public class BuyerRepository:IBuyerRepository
    {
        private readonly AppDbContext _context;

        public BuyerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddInterestAsync(Interest interest)
        {
            await _context.Interests.AddAsync(interest);
        }

        public async Task<List<Interest>> GetBuyerInterestsAsync(Guid buyerId)
        {
            return await _context.Interests
                .Include(i => i.Item)
                    .ThenInclude(item => item.ItemName)
                .Where(i => i.BuyerId == buyerId)
                .ToListAsync();
        }

        public async Task<List<Item>> FilterItemsAsync(string? itemName, string? city)
        {
            IQueryable<Item> query = _context.Items
          .Include(i => i.ItemName)
          .Include(i => i.Images)
          .Include(i => i.Seller)
              .ThenInclude(s => s.Profile); // 🔥 REQUIRED

            if (!string.IsNullOrWhiteSpace(itemName))
                query = query.Where(i => i.ItemName.Name.Contains(itemName));

            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(i => i.Seller.Profile.City == city);

            return await query.ToListAsync();
        }


        public async Task<bool> InterestExistsAsync(Guid buyerId, Guid itemId)
        {
            return await _context.Interests
                .AnyAsync(i => i.BuyerId == buyerId && i.ItemId == itemId);
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _context.Items
        .Include(i => i.ItemName)
        .Include(i => i.Images)
        .Include(i => i.Seller)
            .ThenInclude(s => s.Profile)
        .Where(i => i.Status != ItemStatus.Removed)
        .ToListAsync();





        }



        public async Task<Item?> GetItemByIdAsync(Guid itemId)
        {
            return await _context.Items
                .FirstOrDefaultAsync(i => i.Id == itemId);
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
