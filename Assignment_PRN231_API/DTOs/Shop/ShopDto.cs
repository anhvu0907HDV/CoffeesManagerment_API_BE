using Assignment_PRN231_API.DTOs.Owner;

namespace Assignment_PRN231_API.DTOs.Shop
{
    public class ShopDto
    {
        public int? ShopId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public List<StaffOwnerDto> Staffs { get; set; } = new List<StaffOwnerDto>();
    }
}
