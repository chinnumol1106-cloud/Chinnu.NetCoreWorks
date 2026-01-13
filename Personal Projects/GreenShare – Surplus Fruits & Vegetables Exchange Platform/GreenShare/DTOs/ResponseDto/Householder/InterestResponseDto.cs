using Domain.Enum;

namespace GreenShare.DTOs.ResponseDto.Householder
{
    public class InterestResponseDto
    {
        public Guid InterestId { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }

        public Guid BuyerId { get; set; }
        public string BuyerName { get; set; }

        public InterestStatus InterestStatus { get; set; }
    }
}
