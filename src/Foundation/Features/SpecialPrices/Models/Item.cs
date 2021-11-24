namespace Foundation.Features.SpecialPrices.Models
{
    public class Item
    {
        public string SKU { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal? Price { get; set; }
        public Discount Discount { get; set; }
    }
}