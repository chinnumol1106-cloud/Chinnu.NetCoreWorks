using imageupload.Enums;

namespace imageupload.Requestobject.Householder
{
    public class CreateItemRequest
    {

        public Guid ItemTypeId { get; set; }
        public Guid ItemNameId { get; set; }

        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public bool IsOrganic { get; set; }
        public SeasonStatus SeasonStatus { get; set; }

        public List<IFormFile> Images { get; set; }

    }
}
