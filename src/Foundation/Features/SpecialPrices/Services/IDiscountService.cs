using Foundation.Features.SpecialPrices.Models;

namespace Foundation.Features.SpecialPrices.Services
{
    public interface IDiscountService
    {
        Discount GetDiscountBySku(string sku);

        bool TryGetDiscountBySku(string sku, out Discount discount);
    }
}
