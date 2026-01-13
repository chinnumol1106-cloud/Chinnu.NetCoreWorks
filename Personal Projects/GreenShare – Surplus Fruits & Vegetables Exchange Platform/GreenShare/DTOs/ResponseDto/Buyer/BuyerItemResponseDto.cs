using Domain.Enum;

namespace GreenShare.DTOs.ResponseDto.Buyer
{
    public class BuyerItemResponseDto
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }

        public decimal TotalPrice { get; set; }   // ✅ CALCULATED
        public bool IsOrganic { get; set; }
        public string City { get; set; }
        public string? ImagePath { get; set; }

        public ItemStatus ItemStatus { get; set; }


        public string SellerName { get; set; }
        public string SellerEmail { get; set; }
        public string SellerPhone { get; set; }

    }
}
