using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CollectionRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid AppleOwnerId { get; set; }          // Householder / Landlord
        public Guid AppleVarietyId { get; set; }

        public decimal QuantityKg { get; set; }

        public CollectionStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(AppleOwnerId))]
        public User User { get; set; }


        [ForeignKey(nameof(AppleVarietyId))]
        public AppleVariety AppleVariety { get; set; }

        public string? ImageUrl { get; set; }

        public int WeekNumber { get; set; }   // ✅ NEW

        public DateTime? PreferredPickupAt { get; set; }
    }
}
