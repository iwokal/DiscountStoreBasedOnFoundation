namespace Foundation.Features.SpecialPrices.Services
{
    public interface IPriceService
    {
        double GetPriceBySku(string sku);
    }
}
