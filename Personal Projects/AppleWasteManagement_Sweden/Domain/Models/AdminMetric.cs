using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AdminMetric
    {
        public Guid Id { get; set; }
        public decimal TotalKgCollected { get; set; }
        public decimal WasteReducedKg { get; set; }

        public DateTime CalculatedAt { get; set; }  
    }
}
