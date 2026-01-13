using AutoMapper;
using Domain.Interfaces.Buyer;
using GreenShare.API.Auth;
using GreenShare.Controllers;
using GreenShare.DTOs.RequestDto.Buyer;
using GreenShare.DTOs.ResponseDto.Buyer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.Exceptions;

namespace GreenShare.API.Buyer
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Buyer")]
    public class BuyerController : BaseApiController<BuyerController>
    {

        private readonly IBuyerService _service;
        private readonly IMapper _mapper;

        public BuyerController(IBuyerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("send-interest")]
        public async Task<IActionResult> SendInterest(SendInterestRequestDto dto)
        {
            var buyerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           
                await _service.SendInterestAsync(buyerId, dto.ItemId);
                return Ok("Interest sent successfully (Pending)");
            

            


        }

        [HttpGet("my-interests")]
        public async Task<IActionResult> MyInterests()
        {
            var buyerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var interests = await _service.GetMyInterestsAsync(buyerId);
            return Ok(_mapper.Map<List<BuyerInterestResponseDto>>(interests));
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterItems(ItemFilterRequestDto dto)
        {
            var items = await _service.FilterItemsAsync(dto.ItemName, dto.City);
            return Ok(_mapper.Map<List<BuyerItemResponseDto>>(items));
        }

        [HttpGet("ViewAllitems")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _service.GetAllItemsAsync();
            return Ok(_mapper.Map<List<BuyerItemResponseDto>>(items));


        }

    }
}
