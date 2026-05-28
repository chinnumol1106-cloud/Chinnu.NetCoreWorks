namespace GreenShare.DTOs.RequestDto.Admin
{
    public class AddCompanyPriceRequestDto
    {
        public Guid? AppleVarietyId { get; set; }
        public Guid AppleGradeId { get; set; }
        public decimal PricePerKg { get; set; }
    }
}
