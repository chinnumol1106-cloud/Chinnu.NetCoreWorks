using AutoMapper;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Interfaces.Admin;
using Domain.Models;
using Domain.Repositories.Admin;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Admin
{
    public class AdminService:IAdminService
    {
        private readonly IAdminRepository _repo;

        public AdminService(IAdminRepository repo)
        {
            _repo = repo;
        }

        public async Task AddItemTypeAsync(ItemType itemType)
        {
            await _repo.AddItemTypeAsync(itemType);
            await _repo.SaveAsync();
        }

        public async Task AddItemNameAsync(ItemName itemName)
        {
            await _repo.AddItemNameAsync(itemName);
            await _repo.SaveAsync();
        }

        public async Task<List<ItemType>> GetItemTypesAsync()
            => await _repo.GetItemTypesAsync();

        public async Task<List<ItemName>> GetItemNamesByTypeAsync(Guid itemTypeId)
            => await _repo.GetItemNamesByTypeAsync(itemTypeId);

        public async Task UpdateUserStatusAsync(Guid userId, bool isActive)
        {
            var user = await _repo.GetUserByIdAsync(userId)
                ?? throw new BusinessException("User not found");

            user.IsActive = isActive;
            await _repo.SaveAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
            => await _repo.GetAllUsersAsync();

        public async Task<List<Interest>> GetAllInterestsAsync()
            => await _repo.GetAllInterestsAsync();


        public async Task<int> RemoveAllSoldItemsAsync()
        {
            var soldItems = await _repo.GetSoldItemsAsync();

            if (!soldItems.Any())
                throw new BusinessException("No sold items found");

            foreach (var item in soldItems)
            {
                item.Status = ItemStatus.Removed;
            }

            await _repo.SaveAsync();
            return soldItems.Count;
        }

    }
}
