namespace GreenShare.DTOs.RequestDto.Company
{
    public class UsageReportRequestDto
    {
        public Guid AppleGradeId { get; set; }
        public decimal QuantityUsedKg { get; set; }
        public string Purpose { get; set; } // Jam / Juice / Biogas
    }
}
