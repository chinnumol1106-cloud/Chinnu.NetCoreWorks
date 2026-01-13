using Domain.Enum;

namespace GreenShare.DTOs.ResponseDto.Householder
{
    public class MyItemInterestDto
    {
        public Guid InterestId { get; set; }
        public string BuyerName { get; set; }
        public InterestStatus InterestStatus { get; set; }
    }
}
