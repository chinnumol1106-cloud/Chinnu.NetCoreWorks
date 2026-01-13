using imageupload.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace imageupload.Models
{
    public class Item
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid SellerId { get; set; }

        [ForeignKey(nameof(SellerId))]
        public User Seller { get; set; }

        public Guid ItemTypeId { get; set; }

        [ForeignKey(nameof(ItemTypeId))]
        public ItemType ItemType { get; set; }

        public Guid ItemNameId { get; set; }


        [ForeignKey(nameof(ItemNameId))]
        public ItemName ItemName { get; set; }

        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public bool IsOrganic { get; set; }
        public SeasonStatus SeasonStatus { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ItemImage> Images { get; set; }
        public ICollection<Interest> Interests { get; set; }

    }
}
