using imageupload.Enums;

namespace imageupload.DTO
{
    public class InterestResponseDto
    {

        public Guid InterestId { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }

        public Guid BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerPhone { get; set; }
        public InterestStatus Status { get; set; }

    }
}
