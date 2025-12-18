using System.ComponentModel.DataAnnotations;

namespace RAZOR_LOGIN_DTO_CFA_.DTOs
{
    public class LoginDto
    {

        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
