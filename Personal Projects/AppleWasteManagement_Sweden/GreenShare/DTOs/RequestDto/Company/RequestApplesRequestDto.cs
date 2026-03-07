namespace GreenShare.DTOs.RequestDto.Company
{
    public class RequestApplesRequestDto
    {
        public Guid AppleGradeId { get; set; }
        public Guid? AppleVarietyId { get; set; }
        public decimal QuantityKg { get; set; }
    }
}
