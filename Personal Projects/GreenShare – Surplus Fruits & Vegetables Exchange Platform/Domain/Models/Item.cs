using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Item
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid SellerId { get; set; }

        [ForeignKey(nameof(SellerId))]
        public User Seller { get; set; }


        public ItemStatus Status { get; set; } = ItemStatus.Available;

        public Guid ItemTypeId { get; set; }

        [ForeignKey(nameof(ItemTypeId))]
        public ItemType ItemType { get; set; }

        public Guid ItemNameId { get; set; }


        [ForeignKey(nameof(ItemNameId))]
        public ItemName ItemName { get; set; }

        

        // 🔴 ADDED
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
        public string Description { get; set; }
        public bool IsOrganic { get; set; }
       
        public SeasonStatus SeasonStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Success;



        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ItemImage> Images { get; set; }
        public ICollection<Interest> Interests { get; set; }

    }
}
