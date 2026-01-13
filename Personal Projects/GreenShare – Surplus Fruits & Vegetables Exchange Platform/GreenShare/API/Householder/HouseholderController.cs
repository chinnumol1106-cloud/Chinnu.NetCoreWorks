using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces.Householder;
using Domain.Models;

using GreenShare.API.Auth;
using GreenShare.Controllers;
using GreenShare.DTOs.RequestDto.Householder;
using GreenShare.DTOs.ResponseDto.Householder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;



namespace GreenShare.API.Householder
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Householder")]
    public class HouseholderController : BaseApiController<HouseholderController>
    {

        private readonly IHouseholderService _service;
        private readonly IMapper _mapper;

        public HouseholderController(
            IHouseholderService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("items")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddItem([FromForm] CreateItemRequestDto dto)
        {
            var sellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var item = _mapper.Map<Item>(dto);
            item.SellerId = sellerId;
            item.Images = new List<ItemImage>();

            var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var imageFolder = Path.Combine(webRoot, "item-images");

            if (!Directory.Exists(imageFolder))
                Directory.CreateDirectory(imageFolder);

            foreach (var image in dto.Images)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                var filePath = Path.Combine(imageFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);

                item.Images.Add(new ItemImage
                {
                    ImagePath = $"item-images/{fileName}"
                });
            }

            await _service.AddItemAsync(item);
            return Ok("Item posted");
        }

        // ✅ VIEW MY ITEMS + INTERESTS
        [HttpGet("items")]
        public async Task<IActionResult> MyItems()
        {
            var sellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var items = await _service.GetMyItemsAsync(sellerId);

            return Ok(_mapper.Map<List<MyItemResponseDto>>(items));
        }

        // ✅ ACCEPT INTEREST
        [HttpPut("interests/{interestId}/accept")]
        public async Task<IActionResult> AcceptInterest(Guid interestId)
        {
            var sellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.AcceptInterestAsync(sellerId, interestId);
            return Ok("Interest accepted & item sold");
        }

        // ✅ REJECT INTEREST
        [HttpPut("interests/{interestId}/reject")]
        public async Task<IActionResult> RejectInterest(Guid interestId)
        {
            var sellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.RejectInterestAsync(sellerId, interestId);
            return Ok("Interest rejected");
        }

        // ✅ MARK SOLD MANUALLY
        [HttpPut("items/{itemId}/sold")]
        public async Task<IActionResult> MarkSold(Guid itemId)
        {
            var sellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.MarkItemAsSoldAsync(sellerId, itemId);
            return Ok("Item marked as sold");
        }
    }
}
