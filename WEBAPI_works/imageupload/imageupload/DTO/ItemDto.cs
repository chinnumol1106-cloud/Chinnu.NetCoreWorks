namespace imageupload.DTO
{
    public class ItemDto
    {
        public Guid Id { get; set; }
        public string ItemType { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public bool IsOrganic { get; set; }
        public string ImageUrl { get; set; }

    }
}
