using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces.Householder;
using Domain.Models;

using GreenShare.API.Auth;
using GreenShare.Controllers;
using GreenShare.DTOs.RequestDto.Householder;
using GreenShare.DTOs.ResponseDto.AppleOwner;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;



namespace GreenShare.API.Householder
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Householder,Landlord")]
    public class AppleOwnerController : BaseApiController<AppleOwnerController>
    {

        private readonly IAppleOwnerService _service;
        private readonly IMapper _mapper;

        public AppleOwnerController(IAppleOwnerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("apple-request")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateRequest(CreateCollectionRequestDto dto)
        {
            var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var request = _mapper.Map<CollectionRequest>(dto);

            await _service.CreateCollectionRequestAsync(ownerId, request,dto.Image);
            return Ok("Collection request created");
        }

        [HttpGet("my-requests")]
        public async Task<IActionResult> MyRequests()
        {
            var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var requests = await _service.GetMyRequestsAsync(ownerId);

            var response = _mapper.Map<List<CollectionRequestResponseDto>>(requests);
            return Ok(response);
        }

      




        [HttpGet("request/{id}/assigned-student")]
        public async Task<IActionResult> AssignedStudent(Guid id)
        {
            var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var assignment = await _service.GetAssignedStudentForRequestAsync(ownerId, id);

            if (assignment == null || assignment.Student == null)
                return Ok("No student assigned yet");

            if (assignment.Student.Profile == null)
                return Ok("Student profile not completed");

            var response = new AssignedStudentResponseDto
            {
                StudentName = assignment.Student.Name,
                PhoneNumber = assignment.Student.Profile.PhoneNumber,
                AssignedAt = assignment.AssignedAt
            };

            return Ok(response);
        }


        [HttpGet("request/{id}/payment-details")]
        public async Task<IActionResult> GetPayment(Guid id)
        {
            var total = await _service.GetTotalPaymentAsync(id);

            return Ok(new
            {
                totalAmount = total
            });
        }

        [HttpPut("request/{id}/cancel")]
        public async Task<IActionResult> CancelRequest(Guid id)
        {
            var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.CancelRequestAsync(ownerId, id);

            return Ok("Request cancelled");
        }




        //[HttpGet("payment-details")]
        //public async Task<IActionResult> PaymentDetails()
        //{
        //    var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        //    var data = await _service.GetPaymentDetailsAsync(ownerId);

        //    return Ok(data);
        //}




    }
}
