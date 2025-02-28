using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.DTOs.Owner
{
    public class ManagerEditDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string PhoneNo { get; set; }
        public Inventory? Inventtory { get; set; }

    }
}
