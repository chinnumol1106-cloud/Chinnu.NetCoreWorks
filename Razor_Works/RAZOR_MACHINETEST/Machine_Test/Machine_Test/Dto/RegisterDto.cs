using System.ComponentModel.DataAnnotations;

namespace Machine_Test.Dto
{
    public class RegisterDto
    {

       
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(8)]
        public string Password { get; set; }
    }
}
