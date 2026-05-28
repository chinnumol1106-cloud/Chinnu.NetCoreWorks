namespace GreenShare.DTOs.ResponseDto.Admin
{
    public class CompanyPriceResponseDto
    {

        public Guid Id { get; set; }
        public string? VarietyName { get; set; }
        public string GradeName { get; set; }
        public decimal PricePerKg { get; set; }
    }
}
