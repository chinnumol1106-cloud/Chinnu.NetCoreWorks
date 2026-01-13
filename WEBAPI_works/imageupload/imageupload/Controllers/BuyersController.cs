using imageupload.Data;
using imageupload.DTO;
using imageupload.Enums;
using imageupload.Models;
using imageupload.Requestobject.Buyer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace imageupload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Buyer")]
    public class BuyersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BuyersController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("send-interest")]
        public IActionResult SendInterest([FromBody] SendInterestRequest request)
        {
            var buyerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var interest = new Interest
            {
                ItemId = request.ItemId,
                BuyerId = buyerId,
                Status = InterestStatus.Pending
            };

            _db.Interests.Add(interest);
            _db.SaveChanges();

            return Ok("Interest sent successfully (Pending)");
        }


        [HttpGet("my-interests")]
        public IActionResult MyInterests()
        {
            var buyerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var interests = _db.Interests
                .Where(i => i.BuyerId == buyerId)
                .Select(i => new InterestResponseDto
                {
                    InterestId = i.Id,
                    ItemId = i.ItemId,
                    ItemName = i.Item.ItemName.Name,
                    Status = i.Status
                })
                .ToList();

            return Ok(interests);
        }


        [HttpPost("filter")]
        public async Task<IActionResult> FilterItems(ItemFilterRequest request)
        {
            var query = _db.Items
                .Include(i => i.ItemName)
                .Include(i => i.Images)
                .Include(i => i.Seller)
                    .ThenInclude(s => s.Profile)
                .AsQueryable();

            // 🔹 Filter by Item Name
            if (!string.IsNullOrWhiteSpace(request.ItemName))
            {
                query = query.Where(i =>
                    i.ItemName.Name.Contains(request.ItemName));
            }

            // 🔹 Filter by City
            if (!string.IsNullOrWhiteSpace(request.City))
            {
                query = query.Where(i =>
                    i.Seller.Profile.City == request.City);
            }

            var items = await query
                .Select(i => new BuyerItemResponseDto
                {
                    ItemId = i.Id,
                    ItemName = i.ItemName.Name,
                    Quantity = i.Quantity,
                    IsOrganic = i.IsOrganic,
                    City = i.Seller.Profile.City,
                    ImagePath = i.Images.FirstOrDefault().ImagePath
                })
                .ToListAsync();

            return Ok(items);
        }



    }
}
