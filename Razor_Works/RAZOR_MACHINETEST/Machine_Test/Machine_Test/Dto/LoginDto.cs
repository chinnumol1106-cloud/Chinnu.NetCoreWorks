using System.ComponentModel.DataAnnotations;

namespace Machine_Test.Dto
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(8)]
        public string Password { get; set; }
    }
}
