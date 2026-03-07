using AutoMapper;
using Domain.Enum;
using Domain.Interfaces.Admin;
using Domain.Models;
using Domain.Services.Admin;
using GreenShare.Controllers;
using GreenShare.DTOs.RequestDto.Admin;
using GreenShare.DTOs.ResponseDto.Admin;
using GreenShare.DTOs.ResponseDto.Company;
using GreenShare.DTOs.ResponseDto.Student;
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

        [HttpPost("apple-types")]
        public async Task<IActionResult> AddAppleType(AddAppleTypeRequestDto dto)
        {
            var type = _mapper.Map<AppleType>(dto);
            await _service.AddAppleTypeAsync(type);
            return Ok("Apple type added");
        }

        [HttpPost("apple-varieties")]
        public async Task<IActionResult> AddAppleVariety(AddAppleVarietyRequestDto dto)
        {
            var variety = _mapper.Map<AppleVariety>(dto);
            await _service.AddAppleVarietyAsync(variety);
            return Ok("Apple variety added");
        }

        [HttpPost("apple-grades")]
        public async Task<IActionResult> AddAppleGrade(AddAppleGradeRequestDto dto)
        {


         
            var grade = _mapper.Map<AppleGrade>(dto);
            await _service.AddAppleGradeAsync(grade);
            return Ok("Apple grade added");
        }

        [HttpPost("buying-prices")]
        public async Task<IActionResult> AddApplePrice(AddApplePriceRequestDto dto)
        {
            var price = _mapper.Map<ApplePrice>(dto);
            await _service.AddApplePriceAsync(price);
            return Ok("Price added");
        }
        [HttpPost("selling-prices")]
        public async Task<IActionResult> AddCompanyPrice(AddCompanyPriceRequestDto dto)
        {
            var price = _mapper.Map<CompanyApplePrice>(dto);
            await _service.AddCompanyPriceAsync(price);
            return Ok("Company price added");
        }


        //get

        [HttpGet("buying-prices")]
        public async Task<IActionResult> GetOwnerPrices()
        {
            var prices = await _service.GetOwnerPricesAsync();

            var response = _mapper.Map<List<OwnerPriceResponseDto>>(prices);

            return Ok(response);
        }
        [HttpGet("selling-prices")]
        public async Task<IActionResult> GetCompanyPrices()
        {
            var prices = await _service.GetCompanyPricesAsync();
            return Ok(_mapper.Map<List<CompanyPriceResponseDto>>(prices));
        }

        [HttpGet("collection-requests")]
        public async Task<IActionResult> GetCollectionRequests([FromQuery] CollectionStatus? status)
        {
            var requests = await _service.GetCollectionRequestsAsync(status);

            var response = _mapper.Map<List<AdminCollectionRequestResponseDto>>(requests);

            return Ok(response);
        }


        [HttpGet("availableApples")]
        public async Task<IActionResult> Available()
    => Ok(await _service.GetAvailableApplesAsync());


        [HttpGet("company-requests")]
        public async Task<IActionResult> CompanyRequests()
        {
            var data = await _service.GetCompanyRequestsAsync();

            var response = data.Select(x => new AdminCompanyRequestResponseDto
            {
                RequestId = x.Id,
                CompanyName = x.Company.User.Name,
                CompanyType = x.Company.CompanyType.ToString(),
                GradeName = x.AppleGrade.Grade,
                VarietyName = x.AppleVariety != null ? x.AppleVariety.Name : "Mixed",
                QuantityKg = x.QuantityKg,
                TotalAmount = x.TotalAmount,
                Status = x.Status.ToString(),
                RequestedAt = x.RequestedAt
            }).ToList();

            return Ok(response);
        }

        [HttpGet("available-students")]
        public async Task<IActionResult> GetAvailableStudents(DayOfWeek day, int weekNumber)
        {
            var availabilities = await _service.GetAvailableStudentsByDayAsync(day, weekNumber);

            var response = availabilities.Select(a => new AdminAvailableStudentResponseDto
            {
                StudentId = a.StudentId,
                StudentName = a.Student.Name,
                Email = a.Student.Email,
                PhoneNumber = a.Student.Profile?.PhoneNumber,
                City = a.Student.Profile?.City
            }).ToList();

            return Ok(response);
        }
        [HttpGet("graded-stock")]
        public async Task<IActionResult> GradedStock()
    => Ok(await _service.GetAvailableApplesAsync());

        [HttpGet("student-assignments")]
        public async Task<IActionResult> GetStudentAssignments([FromQuery] AssignmentStatus? status)
        {
            var assignments = await _service.GetStudentAssignmentsAsync(status);

            var response = _mapper.Map<List<AdminStudentAssignmentResponseDto>>(assignments);

            return Ok(response);
        }
       

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.GetUsersAsync();
            return Ok(_mapper.Map<List<UserResponseDto>>(users));
        }

        [HttpGet("metrics")]
        public async Task<IActionResult> GetMetrics()
        {
            var metrics = await _service.GetMetricsAsync();
            return Ok(_mapper.Map<MetricsResponseDto>(metrics));
        }
     
        //put


        [HttpPut("buying-prices/{id}")]
        public async Task<IActionResult> UpdateApplePrice(Guid id,decimal newPrice)
        {
            await _service.UpdateApplePriceAsync(id, newPrice);
            return Ok("Price updated successfully");
        }

       

        [HttpPut("selling-prices/{gradeId}")]
        public async Task<IActionResult> UpdateCompanyPrice(Guid gradeId, decimal newPrice)
        {
            await _service.UpdateCompanyPriceAsync(gradeId, newPrice);
            return Ok("Company price updated");
        }

       

        [HttpPut("assign-student")]
        public async Task<IActionResult> AssignStudent(AssignStudentRequestDto dto)
        {
            await _service.AssignStudentAsync(dto.StudentId, dto.CollectionRequestId);
            return Ok("Student assigned");
        }



        [HttpPut("company-requests/{id}/approve")]
        public async Task<IActionResult> Approve(Guid id)
        {
            await _service.ApproveCompanyRequestAsync(id);
            return Ok("Approved");
        }

        [HttpPut("company-requests/{id}/reject")]
        public async Task<IActionResult> Reject(Guid id)
        {
            await _service.RejectCompanyRequestAsync(id);
            return Ok("Rejected");
        }

        [HttpPut("company-requests/{id}/confirm-payment")]
        public async Task<IActionResult> ConfirmPayment(Guid id)
        {
            await _service.ConfirmCompanyPaymentAsync(id);
            return Ok("Payment Confirmed");
        }


        [HttpPut("block-user/{id}")]
        public async Task<IActionResult> BlockUser(Guid id)
        {
            await _service.BlockUserAsync(id);
            return Ok("User blocked");
        }
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _service.DeleteUserAsync(id);
            return Ok("User deleted successfully");
        }

    }
}
