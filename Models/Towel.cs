namespace CannonPackingAPI.Models
{
    public class Towel
    {
        public int Id { get; set; }
        public string ItemCode { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<BoxTowel> BoxTowels { get; set; } = new List<BoxTowel>();
    }
}
