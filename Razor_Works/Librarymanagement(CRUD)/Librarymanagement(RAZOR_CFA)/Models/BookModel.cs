using System.ComponentModel.DataAnnotations;

namespace Librarymanagement_RAZOR_CFA_.Models
{
    public class BookModel
    {
        [Key]
        public int BookId {  get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly PublishDate {  get; set; }
    }
}
