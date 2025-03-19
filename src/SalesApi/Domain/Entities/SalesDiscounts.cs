namespace SalesApi.Domain.Entities
{
    public class DiscountConfiguration
    {
        public int Id { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
