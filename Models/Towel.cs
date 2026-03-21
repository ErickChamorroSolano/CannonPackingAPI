namespace CannonPackingAPI.Models
{
    public class Towel
    {
        public int Id { get; set; }
        public string ItemCode { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public string TowelStatus { get; set; } = Common.Enums.TowelStatus.LOOSE.ToString();
        public bool IsActive { get; set; } = true;
        public ICollection<BoxTowel> BoxTowels { get; set; } = new List<BoxTowel>();
    }
}
