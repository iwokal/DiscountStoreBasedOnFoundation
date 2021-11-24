namespace Foundation.Features.SpecialPrices.Models
{
    public class Item
    {
        public string SKU { get; set; }
        public int Quantity { get; set; } = 1;
        public double Price { get; set; }
        public Discount Discount { get; set; }
    }
}