namespace GreenShare.DTOs.ResponseDto.Admin
{
    public class AdminAvailableStudentResponseDto
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }   // ✅ contact
        public string City { get; set; }           // ✅ location
    }
}
