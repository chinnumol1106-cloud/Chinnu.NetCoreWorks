using System.ComponentModel.DataAnnotations;

namespace Exercise_RAZOR_Book_.Model
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
      
        public string Author { get; set; }

        public DateTime PublishedDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Publisher { get; set; }
    }
}
