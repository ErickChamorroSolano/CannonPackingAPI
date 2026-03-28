namespace CannonPackingAPI.DTOs
{
    public class BoxDto
    {
        public string BoxCode { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public short Capacity { get; set; }
    }
}
