using imageupload.Data;
using imageupload.DTO;
using imageupload.Enums;
using imageupload.Models;
using imageupload.Requestobject.Householder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace imageupload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Seller")]
    public class SellerController : ControllerBase
    {


        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public SellerController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        // 🔹 ADD ITEM WITH IMAGES
        [HttpPost("add-item")]
      
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddItem([FromForm] CreateItemRequest request)
        {
            // 🔐 Get logged-in seller id from JWT
            var sellerIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(sellerIdClaim))
                return Unauthorized("Seller id not found in token");

            var sellerId = Guid.Parse(sellerIdClaim);

            // ✅ Validate ItemType
            if (!_db.ItemTypes.Any(x => x.Id == request.ItemTypeId))
                return BadRequest("Invalid ItemType");

            // ✅ Validate ItemName belongs to ItemType
            if (!_db.ItemNames.Any(x =>
                x.Id == request.ItemNameId &&
                x.ItemTypeId == request.ItemTypeId))
                return BadRequest("Invalid ItemName for given ItemType");

            // 🧱 Create Item entity
            var item = new Item
            {
                SellerId = sellerId,
                ItemTypeId = request.ItemTypeId,
                ItemNameId = request.ItemNameId,
                Quantity = request.Quantity,
                Description = request.Description,
                IsOrganic = request.IsOrganic,
                SeasonStatus = request.SeasonStatus,
                CreatedAt = DateTime.UtcNow,
                Images = new List<ItemImage>()
            };

            // 🔥 SAFE WEB ROOT (fixes your error)
            var webRootPath = _env.WebRootPath
                              ?? Path.Combine(_env.ContentRootPath, "wwwroot");

            var imageRootPath = Path.Combine(webRootPath, "item-images");

            if (!Directory.Exists(imageRootPath))
                Directory.CreateDirectory(imageRootPath);

            // 📸 Save images
            if (request.Images != null && request.Images.Any())
            {
                foreach (var image in request.Images)
                {
                    if (image.Length == 0)
                        continue;

                    var extension = Path.GetExtension(image.FileName);
                    var fileName = $"{Guid.NewGuid()}{extension}";
                    var filePath = Path.Combine(imageRootPath, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(stream);

                    item.Images.Add(new ItemImage
                    {
                        ImagePath = $"item-images/{fileName}"
                    });
                }
            }

            // 💾 Save to DB
            _db.Items.Add(item);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                message = "Item added successfully",
                itemId = item.Id
            });
        }



        [HttpGet("received-interests")]
        public async Task<IActionResult> ReceivedInterestsAsync()
        {
            var sellerIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(sellerIdClaim))
                return Unauthorized("Seller id not found in token");

            var sellerId = Guid.Parse(sellerIdClaim);

            var interests = await _db.Interests
    .Include(i => i.Item)
        .ThenInclude(item => item.ItemName)
    .Include(i => i.Buyer)   // ✅ ADD THIS
    .Where(i => i.Item.SellerId == sellerId)
    .Select(i => new InterestResponseDto
    {
        InterestId = i.Id,
        ItemId = i.ItemId,
        ItemName = i.Item.ItemName.Name,

        // ✅ ADD THESE
        BuyerId = i.Buyer.Id,
        BuyerName = i.Buyer.Name,
        //BuyerPhone = i.Buyer.Phone,

        Status = i.Status
    })
    .ToListAsync();

            return Ok(interests);
        }



        [HttpPut("{interestId}/accept")]
        public IActionResult AcceptInterest(Guid interestId)
        {
            var interest = _db.Interests.Find(interestId);

            if (interest == null)
                return NotFound("Interest not found");

            interest.Status = InterestStatus.Accepted;
            _db.SaveChanges();

            return Ok("Interest accepted");
        }



        [HttpPut("{interestId}/reject")]
        public IActionResult RejectInterest(Guid interestId)
        {
            var interest = _db.Interests.Find(interestId);

            if (interest == null)
                return NotFound("Interest not found");

            interest.Status = InterestStatus.Rejected;
            _db.SaveChanges();

            return Ok("Interest rejected");
        }





    }
}
