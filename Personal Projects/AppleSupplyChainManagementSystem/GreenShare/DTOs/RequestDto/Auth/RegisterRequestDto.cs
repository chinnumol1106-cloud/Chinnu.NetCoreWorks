using Domain.Enum;

namespace GreenShare.DTOs.RequestDto.Auth
{
    public class RegisterRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public CompanyType? CompanyType { get; set; }
    }
}
