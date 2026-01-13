namespace GreenShare.DTOs.ResponseDto.Householder
{
    public class MyItemResponseDto
    {

        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public bool IsOrganic { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<MyItemInterestDto> Interests { get; set; }

    }
}
