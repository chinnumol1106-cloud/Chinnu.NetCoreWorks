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
        private readonly IWebHostEnvironment _env;

        public ProfileController(
            IProfileService service,
            IMapper mapper,
            IWebHostEnvironment env)
        {
            _service = service;
            _mapper = mapper;
            _env = env;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProfile([FromForm] CreateProfileRequestDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            string? imagePath = null;
            if (dto.ProfileImage != null)
                imagePath = SaveProfileImage(dto.ProfileImage);

            var profile = _mapper.Map<UserProfile>(dto);
            profile.UserId = userId;
            profile.ProfileImagePath = imagePath;

            await _service.CreateProfileAsync(profile);
            return Ok("Profile created successfully");
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           
            
                var profile = await _service.GetMyProfileAsync(userId);
                return Ok(_mapper.Map<UserProfileResponseDto>(profile));
            

           

        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileRequestDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _service.UpdateProfileAsync(userId, profile =>
            {
                profile.Address = dto.Address;
                profile.City = dto.City;
                profile.District = dto.District;
                profile.State = dto.State;
                profile.Bio = dto.Bio;

                if (dto.ProfileImage != null)
                    profile.ProfileImagePath = SaveProfileImage(dto.ProfileImage);
            });

            return Ok("Profile updated successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProfile()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.DeleteProfileAsync(userId);
            return Ok("Profile deleted successfully");
        }

        private string SaveProfileImage(IFormFile image)
        {
            var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var folder = Path.Combine(webRoot, "profile-images");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var path = Path.Combine(folder, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            image.CopyTo(stream);

            return $"profile-images/{fileName}";
        }
    }
}
