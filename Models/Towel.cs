namespace CannonPackingAPI.Models
{
    public class Towel
    {
        public int Id { get; set; }
        public string ItemCode { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public string Status { get; set; } = "LOOSE"; // LOOSE / PACKED
        public int? BoxId { get; set; }
        public Box? Box { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
