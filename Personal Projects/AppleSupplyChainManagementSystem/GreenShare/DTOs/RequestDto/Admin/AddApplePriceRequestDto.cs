using System.ComponentModel.DataAnnotations;

namespace GreenShare.DTOs.RequestDto.Admin
{
    public class AddApplePriceRequestDto
    {
       
        public Guid AppleTypeId { get; set; }

     
        public Guid AppleVarietyId { get; set; }

      
        public Guid AppleGradeId { get; set; }

      
        public decimal PricePerKg { get; set; }
    }
}
