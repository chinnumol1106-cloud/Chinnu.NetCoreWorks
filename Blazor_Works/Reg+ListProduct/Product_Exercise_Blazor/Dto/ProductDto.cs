using System.ComponentModel.DataAnnotations;

namespace Product_Exercise_Blazor.Dto
{
    public class ProductDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
