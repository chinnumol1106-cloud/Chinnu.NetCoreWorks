using imageupload.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace imageupload.Models
{
    public class Interest
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ItemId { get; set; }


        [ForeignKey(nameof(ItemId))]
        public Item Item { get; set; }

        public Guid BuyerId { get; set; }


        [ForeignKey(nameof(BuyerId))]
        public User Buyer { get; set; }

        public InterestStatus Status { get; set; } = InterestStatus.Pending;

    }
}
