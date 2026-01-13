using imageupload.Data;
using imageupload.DTO;
using imageupload.Models;
using imageupload.Requestobject.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace imageupload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {


        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProfileController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // =====================================================
        // CREATE PROFILE
        // =====================================================
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProfile([FromForm] CreateProfileRequest request)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)
            );

            // Prevent duplicate profile
            if (await _context.UserProfiles.AnyAsync(p => p.UserId == userId))
                return BadRequest("Profile already exists");

            string imagePath = null;

            if (request.ProfileImage != null)
            {
                imagePath = SaveProfileImage(request.ProfileImage);
            }

            var profile = new UserProfile
            {
                UserId = userId,
                Address = request.Address,
                City = request.City,
                District = request.District,
                State = request.State,
                Bio = request.Bio,
                ProfileImagePath = imagePath
            };

            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync();

            return Ok("Profile created successfully");
        }

        // =====================================================
        // VIEW MY PROFILE
        // =====================================================
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)
            );

            var profile = await _context.UserProfiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                return NotFound("Profile not found");

            var response = new UserProfileResponseDto
            {
                ProfileId = profile.Id,
                UserId = profile.UserId,
                UserName = profile.User.Name,
                Email = profile.User.Email,
                Address = profile.Address,
                City = profile.City,           // ✅ ADD
                District = profile.District,   // ✅ ADD
                State = profile.State,
                Bio = profile.Bio,
                ProfileImagePath = profile.ProfileImagePath
            };

            return Ok(response);
        }

        // =====================================================
        // UPDATE PROFILE
        // =====================================================
        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileRequest request)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)
            );

            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                return NotFound("Profile not found");

            profile.Address = request.Address;
            profile.Bio = request.Bio;
            profile.City = request.City;         // ✅ ADD
            profile.District = request.District; // ✅ ADD
            profile.State = request.State;

            if (request.ProfileImage != null)
            {
                profile.ProfileImagePath = SaveProfileImage(request.ProfileImage);
            }

            await _context.SaveChangesAsync();

            return Ok("Profile updated successfully");
        }

        // =====================================================
        // DELETE PROFILE
        // =====================================================
        [HttpDelete]
        public async Task<IActionResult> DeleteProfile()
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)
            );

            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                return NotFound("Profile not found");

            _context.UserProfiles.Remove(profile);
            await _context.SaveChangesAsync();

            return Ok("Profile deleted successfully");
        }

        // =====================================================
        // IMAGE SAVE HELPER
        // =====================================================
        private string SaveProfileImage(IFormFile image)
        {
            var webRoot = _env.WebRootPath
                          ?? Path.Combine(_env.ContentRootPath, "wwwroot");

            var folderPath = Path.Combine(webRoot, "profile-images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            image.CopyTo(stream);

            return $"profile-images/{fileName}";
        }



    }
}
