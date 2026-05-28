using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AppleVariety
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }   // Aroma, Ingrid Marie

        public Guid AppleTypeId { get; set; }

        [ForeignKey(nameof(AppleTypeId))]
        public AppleType AppleType { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
