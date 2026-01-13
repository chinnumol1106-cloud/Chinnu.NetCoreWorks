using System.ComponentModel.DataAnnotations;

namespace LoginDto.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
