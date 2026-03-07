using Domain.Enum;

namespace GreenShare.DTOs.ResponseDto.Admin
{
    public class AdminStudentAssignmentResponseDto
    {
        public Guid AssignmentId { get; set; }

        public string StudentName { get; set; }
        public string AppleOwnerName { get; set; }

        public Guid CollectionRequestId { get; set; }

        public AssignmentStatus AssignmentStatus { get; set; }

        public DateTime AssignedAt { get; set; }
        public DateTime? CollectedAt { get; set; }
    }
}
