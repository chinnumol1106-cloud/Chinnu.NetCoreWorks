using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces.Profile;
using Domain.Models;
using GreenShare.DTOs.RequestDto.Profile;
using GreenShare.DTOs.ResponseDto.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GreenShare.API.Profile
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _service;
        private readonly IMapper _mapper;

        public ProfileController(IProfileService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProfileRequestDto dto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var profile = _mapper.Map<Domain.Models.UserProfile>(dto);
            var result = await _service.CreateAsync(userId, profile);
            return Ok(_mapper.Map<ProfileResponseDto>(result));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var profile = await _service.GetMyProfileAsync(userId);
            return Ok(_mapper.Map<ProfileResponseDto>(profile));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProfileRequestDto dto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var profile = _mapper.Map<Domain.Models.UserProfile>(dto);
            var result = await _service.UpdateAsync(userId, profile);
            return Ok(_mapper.Map<ProfileResponseDto>(result));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.DeleteAsync(userId);
            return Ok("Profile deleted successfully");
        }
    }
}
