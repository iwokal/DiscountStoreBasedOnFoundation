namespace Foundation.Features.SpecialPrices.Services
{
    public interface IPriceService
    {
        decimal? GetPriceBySku(string sku);
    }
}
