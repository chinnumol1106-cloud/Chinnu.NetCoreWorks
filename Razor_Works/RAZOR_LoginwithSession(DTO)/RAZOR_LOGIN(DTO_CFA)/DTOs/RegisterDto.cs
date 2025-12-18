using System.ComponentModel.DataAnnotations;

namespace RAZOR_LOGIN_DTO_CFA_.DTOs
{
    public class RegisterDto
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
