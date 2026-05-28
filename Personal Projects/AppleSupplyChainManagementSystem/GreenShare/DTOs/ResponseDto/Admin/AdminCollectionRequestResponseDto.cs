using Domain.Enum;

namespace GreenShare.DTOs.ResponseDto.Admin
{
    public class AdminCollectionRequestResponseDto
    {
        public Guid RequestId { get; set; }

        public string AppleOwnerName { get; set; }
        public string Address { get; set; }

        public string AppleVariety { get; set; }
        public decimal QuantityKg { get; set; }
        public int WeekNumber {  get; set; }

        public DateTime? PreferredPickupAt { get; set; }

        public CollectionStatus Status { get; set; }

        // ✅ NEW
        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
