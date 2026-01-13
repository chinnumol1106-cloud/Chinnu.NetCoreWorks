using System.ComponentModel.DataAnnotations;

namespace imageupload.Models
{
    public class ItemType
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }   // Fruit / Vegetable
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ItemName> ItemNames { get; set; }
    

}
}
