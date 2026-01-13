using System.ComponentModel.DataAnnotations;

namespace LoginDto.DTOs
{
    public class UserDTO
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
