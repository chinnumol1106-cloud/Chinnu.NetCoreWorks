namespace GreenShare.DTOs.ResponseDto.Profile
{
    public class UserProfileResponseDto
    {
        public Guid ProfileId { get; set; }
        public Guid UserId { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }

        public string? Bio { get; set; }
        public string? ProfileImagePath { get; set; }
    }
}
