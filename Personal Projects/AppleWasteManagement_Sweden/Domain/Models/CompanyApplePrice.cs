using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public  class CompanyApplePrice
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid AppleGradeId { get; set; }
        public decimal PricePerKg { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(AppleGradeId))]
        public AppleGrade AppleGrade { get; set; }

        public Guid? AppleVarietyId { get; set; }

        [ForeignKey(nameof(AppleVarietyId))]
        public AppleVariety? AppleVariety { get; set; }
    }
}
