using Domain.Enum;

namespace GreenShare.DTOs.ResponseDto.Pay
{
    public class PaymentResponseDto
    {
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
