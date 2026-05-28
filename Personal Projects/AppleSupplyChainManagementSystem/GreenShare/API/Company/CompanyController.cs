using AutoMapper;
using Domain.Interfaces.Company;

using GreenShare.Controllers;
using GreenShare.DTOs.RequestDto.Company;
using GreenShare.DTOs.ResponseDto.Admin;
using GreenShare.DTOs.ResponseDto.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GreenShare.API.Company
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Company")]
    public class CompanyController : BaseApiController<CompanyController>
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;

       public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

      
        [HttpPost("request")]
        public async Task<IActionResult> Request(RequestApplesRequestDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _service.RequestApplesAsync(
                userId,
                dto.AppleGradeId,
                dto.AppleVarietyId,
                dto.QuantityKg);

            return Ok("Request sent successfully");
        }

      
        [HttpGet("available-apples")]
        public async Task<IActionResult> Grades()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var data = await _service.GetAvailableGradesForCompanyAsync(userId);

            return Ok(data);
        }

        
        [HttpGet("prices")]
        public async Task<IActionResult> Prices()
        {
            var prices = await _service.GetPricesAsync();
            var response = _mapper.Map<List<CompanyPriceResponseDto>>(prices);

            return Ok(response);
        }


     
        [HttpGet("my-requests")]
        public async Task<IActionResult> MyRequests()
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var data = await _service.GetMyRequestsAsync(id);

            var response = data.Select(x => new
            {
                RequestId = x.Id,
                Grade = x.AppleGrade.Grade,
                Variety = x.AppleVariety != null ? x.AppleVariety.Name : "Mixed",
                QuantityKg = x.QuantityKg,
                TotalAmount = x.TotalAmount,
                Status = x.Status.ToString()
            });

            return Ok(response);
        }

      
        [HttpPost("pay/{requestId}")]
        public async Task<IActionResult> Pay(Guid requestId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.PayAsync(userId, requestId);
            return Ok("Payment successful");
        }

    
        [HttpPut("receive/{requestId}")]
        public async Task<IActionResult> Receive(Guid requestId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.ReceiveAsync(userId, requestId);
            return Ok("Stock updated");
        }


        [HttpGet("stock")]
        public async Task<IActionResult> Stock()
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var data = await _service.GetStockAsync(id);

            var response = data.Select(x => new
            {
                Grade = x.AppleGrade.Grade,
                QuantityKg = x.QuantityKg
            });

            return Ok(response);
        }

       
        [HttpPost("usage")]
        public async Task<IActionResult> Usage( UsageReportRequestDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.UsageAsync(userId, dto.AppleGradeId, dto.QuantityUsedKg, dto.Purpose);
            return Ok("Usage recorded");
        }


    }
}
