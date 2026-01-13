using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ItemType
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }   
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ItemName> ItemNames { get; set; }

    }
}
