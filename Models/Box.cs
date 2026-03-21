namespace CannonPackingAPI.Models
{
    public class Box
    {
        public int Id { get; set; }
        public string BoxCode { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string BoxStatus { get; set; } = Common.Enums.BoxStatus.OPEN.ToString();
        public bool IsActive { get; set; } = true;
        public ICollection<BoxTowel> BoxTowels { get; set; } = new List<BoxTowel>();
    }
}
