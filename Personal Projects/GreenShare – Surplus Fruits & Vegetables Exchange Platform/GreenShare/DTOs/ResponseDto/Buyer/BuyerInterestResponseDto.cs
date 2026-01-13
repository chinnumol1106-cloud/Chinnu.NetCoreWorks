using Domain.Enum;

namespace GreenShare.DTOs.ResponseDto.Buyer
{
    public class BuyerInterestResponseDto
    {
        public Guid InterestId { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public InterestStatus InterestStatus { get; set; }
        public ItemStatus ItemStatus { get; set; }
    }
}
