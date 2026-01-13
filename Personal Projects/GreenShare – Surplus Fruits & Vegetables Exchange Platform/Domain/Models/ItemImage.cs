using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
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
