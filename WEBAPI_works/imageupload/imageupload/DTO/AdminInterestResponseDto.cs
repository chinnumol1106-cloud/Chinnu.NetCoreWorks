using imageupload.Enums;

namespace imageupload.DTO
{
    public class AdminInterestResponseDto
    {
        public Guid InterestId { get; set; }

        public Guid ItemId { get; set; }
        public string ItemName { get; set; }

        public Guid SellerId { get; set; }
        public string SellerName { get; set; }

        public Guid BuyerId { get; set; }
        public string BuyerName { get; set; }

        public InterestStatus Status { get; set; }
    }
}
