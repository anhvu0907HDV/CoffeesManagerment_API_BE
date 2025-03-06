namespace Asignment_PRN231_API_FE.ViewModel
{
    public class ShopOwnerVM
    {
        public int? ShopId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public List<StaffOwnerVM> Staffs { get; set; } = new List<StaffOwnerVM>();
    }
}
