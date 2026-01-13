namespace imageupload.Requestobject.Admin
{
    public class UpdateUserStatusRequest
    {
        public Guid UserId { get; set; }
        public bool IsActive {  get; set; }
    }
}
