using Domain.Data;
using Domain.Enum;
using Domain.Interfaces.Householder;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Householder
{
    internal class HouseholderRepository:IHouseholderRepository
    {
        private readonly AppDbContext _context;

        public HouseholderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddItemAsync(Item item)
        {
            //item.Status = ItemStatus.Available;
            await _context.Items.AddAsync(item);
        }
           
          

        public async Task<List<Item>> GetMyActiveItemsAsync(Guid sellerId)
        {
            return await _context.Items
                .Include(i => i.ItemName)
                .Include(i => i.Interests)
                    .ThenInclude(intr => intr.Buyer)
                .Where(i =>
                    i.SellerId == sellerId &&
                    i.Status == ItemStatus.Available)   
                .ToListAsync();
        }

        public async Task<Interest?> GetInterestByIdAsync(Guid interestId)
            => await _context.Interests.FirstOrDefaultAsync(i => i.Id == interestId);

        public async Task<Item?> GetItemByIdAsync(Guid itemId)
        {
            return await _context.Items
                .Include(i => i.Interests)
                .FirstOrDefaultAsync(i => i.Id == itemId);
        }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
