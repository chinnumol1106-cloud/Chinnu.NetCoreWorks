namespace GreenShare.DTOs.ResponseDto.Admin
{
    public class MetricsResponseDto
    {
        public decimal TotalKgCollected { get; set; }
        public decimal WasteReducedKg { get; set; }
        public DateTime CalculatedAt { get; set; }
    }
}
