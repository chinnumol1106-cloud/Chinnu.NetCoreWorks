using imageupload.Data;
using imageupload.DTO;
using imageupload.Models;
using imageupload.Requestobject.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace imageupload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
  
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AdminController(AppDbContext db)
        {
            _db = db;
        }

        // 1️⃣ ADD ITEM TYPE
        [HttpPost("item-types")]
        public IActionResult AddItemType(AddItemTypeRequest request)
        {
            var type = new ItemType
            {
                Name = request.Name
            };

            _db.ItemTypes.Add(type);
            _db.SaveChanges();

            return Ok("Item type added");
        }

        // 2️⃣ ADD ITEM NAME
        [HttpPost("item-names")]
        public IActionResult AddItemName(AddItemNameRequest request)
        {
            var itemName = new ItemName
            {
                ItemTypeId = request.ItemTypeId,
                Name = request.Name
            };

            _db.ItemNames.Add(itemName);
            _db.SaveChanges();

            return Ok("Item name added");
        }

        // 3️⃣ GET ALL ITEM TYPES
        [HttpGet("item-types")]
        public IActionResult GetItemTypes()
        {
            return Ok(_db.ItemTypes.ToList());
        }

        // 4️⃣ GET ITEM NAMES BY TYPE
        [HttpGet("item-names/{itemTypeId}")]
        public IActionResult GetItemNames(Guid itemTypeId)
        {
            var data = _db.ItemNames
                .Where(x => x.ItemTypeId == itemTypeId)
                .Select(x => new ItemNameDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            return Ok(data);
        }


        // 🔹 BLOCK / UNBLOCK USER
        [HttpPut("user-status")]
        public IActionResult UpdateUserStatus(UpdateUserStatusRequest request)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == request.UserId);

            if (user == null)
                return NotFound("User not found");

            user.IsActive = request.IsActive;
            _db.SaveChanges();

            return Ok(request.IsActive ? "User Activated" : "User Blocked");
        }


        // 🔹 GET ALL USERS
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _db.Users
                .Select(u => new UserListDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role,
                    EmailConfirmed = u.EmailConfirmed,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt
                })
                .ToList();

            return Ok(users);
        }


        // 🔹 VIEW ALL INTERESTS (ADMIN)
        [HttpGet("interests")]
        public async Task<IActionResult> GetAllInterests()
        {
            var interests = await _db.Interests
                .Include(i => i.Item)
                    .ThenInclude(item => item.Seller)
                .Include(i => i.Buyer)
                .Select(i => new AdminInterestResponseDto
                {
                    InterestId = i.Id,

                    ItemId = i.ItemId,
                    ItemName = i.Item.ItemName.Name,

                    SellerId = i.Item.SellerId,
                    SellerName = i.Item.Seller.Name,

                    BuyerId = i.BuyerId,
                    BuyerName = i.Buyer.Name,

                    Status = i.Status
                })
                .ToListAsync();

            return Ok(interests);
        }


    }
}