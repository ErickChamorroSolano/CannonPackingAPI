using CannonPackingAPI.Models;

namespace CannonPackingAPI.DTOs
{
    public class BoxesDto
    {
        public int Id { get; set; }
        public string BoxCode { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public short Capacity { get; set; } // smallint
        public short CurrentCount { get; set; } // smallint
        public string Status { get; set; } = "OPEN"; // OPEN / CLOSED
        public bool IsActive { get; set; } = true;
        public ICollection<TowelsDto>? Towels { get; set; } = new List<TowelsDto>();
    }
}
