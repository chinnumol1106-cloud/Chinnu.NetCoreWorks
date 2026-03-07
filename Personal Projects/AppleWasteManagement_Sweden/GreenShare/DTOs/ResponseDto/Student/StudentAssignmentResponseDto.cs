using Domain.Enum;

namespace GreenShare.DTOs.ResponseDto.Student
{
    public class StudentAssignmentResponseDto
    {
        public Guid AssignmentId { get; set; }
        public string AppleOwnerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string AppleVariety { get; set; }
        public int RequestedKg { get; set; }
        public CollectionStatus CollectionStatus { get; set; }
        public AssignmentStatus AssignmentStatus { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
