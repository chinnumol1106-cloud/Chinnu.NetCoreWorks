namespace imageupload.DTO
{
    public class BuyerItemResponseDto
    {

        public Guid ItemId { get; set; }

        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public bool IsOrganic { get; set; }

        // Seller location (for buyer filtering / display)
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }

        public string ImagePath { get; set; }

    }
}
