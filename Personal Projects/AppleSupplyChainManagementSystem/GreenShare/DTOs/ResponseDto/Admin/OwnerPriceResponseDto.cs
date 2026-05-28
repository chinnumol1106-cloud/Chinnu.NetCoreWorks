namespace GreenShare.DTOs.ResponseDto.Admin
{
    public class OwnerPriceResponseDto
    {
        public Guid Id { get; set; }
        public string AppleType { get; set; }
        public string AppleVariety { get; set; }
        public string AppleGrade { get; set; }
        public decimal PricePerKg { get; set; }
    }
}
