using System.ComponentModel.DataAnnotations;

namespace GreenShare.DTOs.RequestDto.Admin
{
    public class AddAppleVarietyRequestDto
    {
       
        public string Name { get; set; }

        
        public Guid TypeId { get; set; }

    }
}
