using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ApplePrice
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid AppleVarietyId { get; set; }
        public Guid AppleGradeId { get; set; }

        public Guid AppleTypeId { get; set; }

        public decimal PricePerKg { get; set; }

        [ForeignKey(nameof(AppleVarietyId))]
        public AppleVariety AppleVariety { get; set; }


        [ForeignKey(nameof(AppleTypeId))]
        public AppleType AppleType { get; set; }


        [ForeignKey(nameof(AppleGradeId))]
        public AppleGrade AppleGrade { get; set; }

      



    }
}
