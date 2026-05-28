using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CompanyUsageReport
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public CompanyEntity Company { get; set; }

        public Guid AppleGradeId { get; set; }

        [ForeignKey(nameof(AppleGradeId))]
        public AppleGrade AppleGrade { get; set; }

        public decimal QuantityKg { get; set; }

        public string Purpose { get; set; } // Jam / Juice / Biogas

        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
    }
}
