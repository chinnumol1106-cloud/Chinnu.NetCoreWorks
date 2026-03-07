using Domain.Enum;

namespace GreenShare.DTOs.ResponseDto.AppleOwner
{
    public class CollectionRequestResponseDto
    {
        public Guid Id { get; set; }
        public string AppleVariety {  get; set; }
        public decimal QuantityKg {  get; set; }
        public CollectionStatus CollectionStatus {  get; set; }
        public DateTime CreatedAt {  get; set; }


        // ✅ NEW
        public string? ImageUrl { get; set; }

        public int WeekNumber { get; set; }   // ✅ NEW

        // ✅ Show pickup availability
        public DateTime? PreferredPickupAt { get; set; }
    }
}
