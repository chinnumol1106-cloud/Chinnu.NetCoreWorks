using BCrypt.Net;
using imageupload.Data;
using imageupload.Models;
using imageupload.Enums;

namespace imageupload.Data.Seed
{
    public static class AdminSeeder
    {
        public static void SeedAdmin(AppDbContext context)
        {


            context.Database.EnsureCreated();


            // If admin already exists → do nothing
            if (context.Users.Any(u => u.Role == UserRole.Admin))
                return;

            var admin = new User
            {
                Name = "System Admin",
                Email = "admin@greenshare.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = UserRole.Admin,
                EmailConfirmed = true
            };

            context.Users.Add(admin);
            context.SaveChanges();
        }
    }
}