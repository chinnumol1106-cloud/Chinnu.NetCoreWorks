using Domain.Data;
using Domain.Enum;
using Domain.Interfaces.Admin;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Admin
{
    public class AdminRepository:IAdminRepository
    {

        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddItemTypeAsync(ItemType itemType)
            => await _context.ItemTypes.AddAsync(itemType);

        public async Task AddItemNameAsync(ItemName itemName)
            => await _context.ItemNames.AddAsync(itemName);

        public async Task<List<ItemType>> GetItemTypesAsync()
            => await _context.ItemTypes.ToListAsync();

        public async Task<List<ItemName>> GetItemNamesByTypeAsync(Guid itemTypeId)
            => await _context.ItemNames
                .Where(x => x.ItemTypeId == itemTypeId)
                .ToListAsync();

        public async Task<List<User>> GetAllUsersAsync()
            => await _context.Users.ToListAsync();

        public async Task<User?> GetUserByIdAsync(Guid userId)
            => await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        public async Task<List<Interest>> GetAllInterestsAsync()
            => await _context.Interests
                .Include(i => i.Item)
                    .ThenInclude(item => item.ItemName)
                .Include(i => i.Item)
                    .ThenInclude(item => item.Seller)
                .Include(i => i.Buyer)
                .ToListAsync();


        public async Task<List<Item>> GetSoldItemsAsync()
        {
            return await _context.Items
                .Where(i => i.Status == ItemStatus.Sold)
                .ToListAsync();
        }


        public async Task SaveAsync()
            => await _context.SaveChangesAsync();

    }
}
