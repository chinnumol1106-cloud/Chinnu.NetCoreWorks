using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces.Buyer;
using Domain.Models;
using GreenShare.API.Auth;
using GreenShare.Controllers;

using GreenShare.DTOs.RequestDto.Student;

using GreenShare.DTOs.ResponseDto.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GreenShare.API.Buyer
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentController : BaseApiController<StudentController>
    {

        private readonly IStudentService _service;
        private readonly IMapper _mapper;

        public StudentController(IStudentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("assignments")]
        public async Task<IActionResult> GetAssignments()
        {
            var studentId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var assignments = await _service.GetAssignmentsAsync(studentId);

            var response = _mapper.Map<List<StudentAssignmentResponseDto>>(assignments);
            return Ok(response);
        }

        [HttpPut("collect/{assignmentId}")]
        public async Task<IActionResult> Collect(Guid assignmentId)
        {
            var studentId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.MarkCollectedAsync(studentId, assignmentId);

            return Ok("Apples collected successfully");
        }

        [HttpPut("grade/{assignmentId}")]
        public async Task<IActionResult> Grade(Guid assignmentId,GradeCollectionRequestDto dto)
        {
            var studentId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var grades = dto.Grades
                .Select(x => (x.AppleVarietyId, x.AppleGradeId, x.QuantityKg))
                .ToList();

            var remaining = await _service.GradeCollectionAsync(
                studentId,
                assignmentId,
                grades);

            return Ok(new
            {
                message = "Grading saved",
                remainingKg = remaining
            });
        }


        [HttpGet("assignments/history")]
        public async Task<IActionResult> GetMyAssignmentHistory()
        {
            var studentId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var history = await _service.GetMyAssignmentHistoryAsync(studentId);

            var response = _mapper.Map<List<StudentAssignmentResponseDto>>(history);

            return Ok(response);
        }


        [HttpPost("availability/week")]
        public async Task<IActionResult> SetWeeklyAvailability(
     WeeklyAvailabilityRequestDto dto)
        {
            var studentId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var list = BuildAvailability(dto);

            await _service.SetWeeklyAvailabilityAsync(studentId, list);

            return Ok("Weekly availability saved");
        }


        private List<StudentAvailability> BuildAvailability(WeeklyAvailabilityRequestDto dto)
        {
            return new List<StudentAvailability>
    {
        new() { DayOfWeek = DayOfWeek.Monday, IsAvailable = dto.Monday, WeekNumber = dto.WeekNumber },
        new() { DayOfWeek = DayOfWeek.Tuesday, IsAvailable = dto.Tuesday, WeekNumber = dto.WeekNumber },
        new() { DayOfWeek = DayOfWeek.Wednesday, IsAvailable = dto.Wednesday, WeekNumber = dto.WeekNumber },
        new() { DayOfWeek = DayOfWeek.Thursday, IsAvailable = dto.Thursday, WeekNumber = dto.WeekNumber },
        new() { DayOfWeek = DayOfWeek.Friday, IsAvailable = dto.Friday, WeekNumber = dto.WeekNumber },
        new() { DayOfWeek = DayOfWeek.Saturday, IsAvailable = dto.Saturday, WeekNumber = dto.WeekNumber },
        new() { DayOfWeek = DayOfWeek.Sunday, IsAvailable = dto.Sunday, WeekNumber = dto.WeekNumber }
    };
        }



        [HttpPut("availability/week")]
        public async Task<IActionResult> UpdateWeeklyAvailability(
            WeeklyAvailabilityRequestDto dto)
        {
            var studentId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var list = BuildAvailability(dto);

            await _service.UpdateWeeklyAvailabilityAsync(studentId, list);

            return Ok("Weekly availability updated");
        }

    }
}
