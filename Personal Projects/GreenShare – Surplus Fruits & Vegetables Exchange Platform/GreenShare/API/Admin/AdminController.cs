using AutoMapper;
using Domain.Interfaces.Admin;
using Domain.Models;
using Domain.Services.Admin;
using GreenShare.Controllers;
using GreenShare.DTOs.RequestDto.Admin;
using GreenShare.DTOs.ResponseDto.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenShare.API.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseApiController<AdminController>
    {
        private readonly IAdminService _service;
        private readonly IMapper _mapper;

        public AdminController(IAdminService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("item-types")]
        public async Task<IActionResult> AddItemType(AddItemTypeRequestDto dto)
        {
            var itemType = _mapper.Map<ItemType>(dto);
            await _service.AddItemTypeAsync(itemType);
            return Ok("Item type added");
        }

        [HttpPost("item-names")]
        public async Task<IActionResult> AddItemName(AddItemNameRequestDto dto)
        {
            var itemName = _mapper.Map<ItemName>(dto);
            await _service.AddItemNameAsync(itemName);
            return Ok("Item name added");
        }

        [HttpGet("item-types")]
        public async Task<IActionResult> GetItemTypes()
        {
            var types = await _service.GetItemTypesAsync();
            return Ok(_mapper.Map<List<ItemTypeDto>>(types));
        }

        [HttpGet("item-names/{itemTypeId}")]
        public async Task<IActionResult> GetItemNames(Guid itemTypeId)
        {
            var names = await _service.GetItemNamesByTypeAsync(itemTypeId);
            return Ok(_mapper.Map<List<ItemNameDto>>(names));
        }

        [HttpPut("user-status")]
        public async Task<IActionResult> UpdateUserStatus(UpdateUserStatusRequestDto dto)
        {
            await _service.UpdateUserStatusAsync(dto.UserId, dto.IsActive);
            return Ok(dto.IsActive ? "User Activated" : "User Blocked");
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(_mapper.Map<List<UserListDto>>(users));
        }

        [HttpGet("interests")]
        public async Task<IActionResult> GetAllInterests()
        {
            var interests = await _service.GetAllInterestsAsync();
            return Ok(_mapper.Map<List<AdminInterestResponseDto>>(interests));
        }

        // ✅ REMOVE ALL SOLD ITEMS
        [HttpPut("items/remove-sold")]
        public async Task<IActionResult> RemoveAllSoldItems()
        {
            var count = await _service.RemoveAllSoldItemsAsync();
            return Ok($"{count} sold items removed successfully");
        }

    }
}
