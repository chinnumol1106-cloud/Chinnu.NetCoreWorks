namespace GreenShare.DTOs.RequestDto.Profile
{
    public class CreateProfileRequestDto
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string? Bio { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
