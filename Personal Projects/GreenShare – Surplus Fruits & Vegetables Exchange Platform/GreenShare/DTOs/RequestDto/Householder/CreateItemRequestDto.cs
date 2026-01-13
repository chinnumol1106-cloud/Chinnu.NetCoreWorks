using Domain.Enum;

namespace GreenShare.DTOs.RequestDto.Householder
{
    public class CreateItemRequestDto
    {
        public Guid ItemTypeId { get; set; }
        public Guid ItemNameId { get; set; }
        public int Quantity { get; set; }          
        public decimal PricePerUnit { get; set; }
        public string Description { get; set; }
        public bool IsOrganic { get; set; }
        public SeasonStatus SeasonStatus { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
