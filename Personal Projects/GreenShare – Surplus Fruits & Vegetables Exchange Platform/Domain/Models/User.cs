using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public UserRole Role { get; set; }

        public bool EmailConfirmed { get; set; }
        public string? EmailConfirmationToken { get; set; }

        public string Phone { get; set; }


        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        public UserProfile Profile { get; set; }   // 1:1
        public ICollection<Item> Items { get; set; }   // Seller → Items
        public ICollection<Interest> Interests { get; set; } // Buyer → Interests


    }
}
