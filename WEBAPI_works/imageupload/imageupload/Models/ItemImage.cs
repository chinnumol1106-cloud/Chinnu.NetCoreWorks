using System.ComponentModel.DataAnnotations.Schema;

namespace imageupload.Models
{
    public class ItemImage
    {


        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ItemId { get; set; }


        [ForeignKey(nameof(ItemId))]
        public Item Item { get; set; }

        public string ImagePath { get; set; }

    }
}
