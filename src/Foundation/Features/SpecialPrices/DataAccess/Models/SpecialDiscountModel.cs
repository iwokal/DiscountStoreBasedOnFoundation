namespace Foundation.Features.SpecialPrices.DataAccess.Models
{
    public class SpecialDiscountModel
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}