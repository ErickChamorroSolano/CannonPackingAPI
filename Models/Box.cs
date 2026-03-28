namespace CannonPackingAPI.Models
{
    public class Box
    {
        public int Id { get; set; }
        public string BoxCode { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public short Capacity { get; set; } // smallint
        public string Status { get; set; } = "OPEN"; // OPEN / CLOSED
        public bool IsActive { get; set; } = true;
        public ICollection<Towel> Towels { get; set; } = new List<Towel>();
    }
}
