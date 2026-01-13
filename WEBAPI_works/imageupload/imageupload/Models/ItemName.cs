using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace imageupload.Models
{
    public class ItemName
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ItemTypeId { get; set; }

        [ForeignKey(nameof(ItemTypeId))]// 🔑 FK → ItemType
        public ItemType ItemType { get; set; }

        public string Name { get; set; } // Apple, Tomato

        public DateTime CreatedAt { get; set; }= DateTime.Now;
    }
}
