using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CollectionResult
    {
        public Guid Id { get; set; }

        public Guid CollectionRequestId { get; set; }


        [ForeignKey(nameof(CollectionRequestId))]
        public CollectionRequest CollectionRequest { get; set; }

        public Guid AppleVarietyId { get; set; }

        [ForeignKey(nameof(AppleVarietyId))]
        public AppleVariety AppleVariety { get; set; }


        public Guid AppleGradeId { get; set; }

        [ForeignKey(nameof(AppleGradeId))]
        public AppleGrade AppleGrade { get; set; }

        public decimal QuantityKg { get; set; }

    }
}
