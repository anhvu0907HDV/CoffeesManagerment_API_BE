namespace Assignment_PRN231_API.DTOs.Owner
{
    public class StaffOwnerDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }
        public string ShopName { get; set; }
        public int? ShopId { get; set; }

    }
}
