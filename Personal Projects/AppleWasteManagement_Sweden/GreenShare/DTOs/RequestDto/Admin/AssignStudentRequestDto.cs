using System.ComponentModel.DataAnnotations;

namespace GreenShare.DTOs.RequestDto.Admin
{
    public class AssignStudentRequestDto
    {
        
        public Guid CollectionRequestId { get; set; }

        
        public Guid StudentId { get; set; }
    }
}
