//using AutoMapper;
//using Domain.Interfaces.Pay;
//using Domain.Models;

//using GreenShare.API.Auth;
//using GreenShare.Controllers;
//using GreenShare.DTOs.RequestDto.Pay;
//using GreenShare.DTOs.ResponseDto.Pay;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace GreenShare.API.Payment
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize(Roles = "Buyer")]
//    public class PaymentController : BaseApiController<PaymentController>
    
//    {
//        private readonly IpaymentService _service;
//        private readonly IMapper _mapper;

//        public PaymentController(IpaymentService service, IMapper mapper)
//        {
//            _service = service;
//            _mapper = mapper;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Pay(PaymentRequestDto dto)
//        {
//            var buyerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

//            var payment = await _service.MakePaymentAsync(buyerId, dto.InterestId);

//            return Ok(_mapper.Map<PaymentResponseDto>(payment));
//        }
//    }
//}
