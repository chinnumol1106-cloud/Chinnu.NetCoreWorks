namespace GreenShare.DTOs.RequestDto.Admin
{
    public class UpdateUserStatusRequestDto
    {

        public Guid UserId { get; set; }
        public bool IsActive { get; set; }

    }
}
