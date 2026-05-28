using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CompanyAppleRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public CompanyEntity Company { get; set; }

        public Guid AppleGradeId { get; set; }

        [ForeignKey(nameof(AppleGradeId))]
        public AppleGrade AppleGrade { get; set; }

        public decimal QuantityKg { get; set; }
        public Guid? AppleVarietyId { get; set; }

        [ForeignKey(nameof(AppleVarietyId))]
        public AppleVariety? AppleVariety { get; set; }

        public bool IsReceived { get; set; } = false;
        public decimal TotalAmount { get; set; }
        public CompanyRequestStatus Status { get; set; } // Pending, Approved, Paid, Completed, Rejected
        public bool IsPaymentConfirmed { get; set; }

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; }
    }
}
