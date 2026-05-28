namespace GreenShare.DTOs.ResponseDto.AppleOwner
{
    public class PaymentTransparencyResponseDto
    {
        public Guid CollectionRequestId { get; set; }
        public decimal TotalCollectedKg { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
