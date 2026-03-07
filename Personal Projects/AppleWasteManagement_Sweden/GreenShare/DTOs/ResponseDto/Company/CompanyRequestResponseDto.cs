namespace GreenShare.DTOs.ResponseDto.Company
{
    public class CompanyRequestResponseDto
    {
        public Guid RequestId { get; set; }
        public string Grade { get; set; }
        public string? Variety {  get; set; }
        public decimal QuantityKg { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        
    }
}
