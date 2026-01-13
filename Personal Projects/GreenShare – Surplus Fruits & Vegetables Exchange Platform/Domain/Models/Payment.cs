using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Payment
    {
         public Guid Id { get; set; }

        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid ItemId { get; set; }

        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(BuyerId))]
        public User? Buyer { get; set; }

        [ForeignKey(nameof(SellerId))]
        public User? Seller { get; set; }

        [ForeignKey(nameof(ItemId))]
        public Item? Item { get; set; }
    }
}
