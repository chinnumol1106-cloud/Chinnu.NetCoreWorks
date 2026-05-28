using Domain.Enum;

namespace GreenShare.DTOs.ResponseDto.Admin
{
    public class AdminCompanyRequestResponseDto
    {
        public Guid RequestId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyType { get; set; }

        public string GradeName { get; set; }

        public string VarietyName { get; set; }

        public decimal QuantityKg { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public DateTime RequestedAt { get; set; }
    }
}
