using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CompanyStock
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public CompanyEntity Company { get; set; }

        public Guid AppleGradeId {  get; set; }

        [ForeignKey(nameof(AppleGradeId))]
        public AppleGrade AppleGrade { get; set; }

        public decimal QuantityKg {  get; set; }
    }
}
