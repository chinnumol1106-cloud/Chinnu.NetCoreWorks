using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Seed
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
                Email = "admin@kommune.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = UserRole.Admin,
                EmailConfirmed = true
            };

            context.Users.Add(admin);
            context.SaveChanges();
        }

    }
}
