namespace GreenShare.DTOs.ResponseDto.Admin
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}


