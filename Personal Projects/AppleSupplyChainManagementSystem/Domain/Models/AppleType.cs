using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AppleType
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }   // Apple
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
