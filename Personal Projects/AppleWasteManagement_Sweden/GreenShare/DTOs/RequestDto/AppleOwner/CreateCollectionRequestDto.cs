using Domain.Enum;

namespace GreenShare.DTOs.RequestDto.Householder
{
    public class CreateCollectionRequestDto
    {
       public Guid AppleVarietyId {  get; set; }
        public decimal QuantityKg {  get; set; }

        // ✅ NEW
        public IFormFile? Image { get; set; }


        public int WeekNumber { get; set; }   // ✅ NEW


        // ✅ AppleOwner availability (date + time)
        public DateTime? PreferredPickupAt { get; set; }
    }
}
