namespace CannonPackingAPI.Models
{
    public class BoxTowel
    {
        public int Id { get; set; }
        public int BoxId { get; set; }
        public Box Box { get; set; } = null!;
        public int TowelId { get; set; }
        public Towel Towel { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
}
