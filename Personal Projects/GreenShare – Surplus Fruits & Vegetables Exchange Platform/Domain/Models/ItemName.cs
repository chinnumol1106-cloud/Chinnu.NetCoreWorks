using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ItemName
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ItemTypeId { get; set; }

        [ForeignKey(nameof(ItemTypeId))]
        public ItemType ItemType { get; set; }

        public string Name { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
