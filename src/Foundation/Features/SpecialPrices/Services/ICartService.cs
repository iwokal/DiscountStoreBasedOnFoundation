using Foundation.Features.SpecialPrices.Models;

namespace Foundation.Features.SpecialPrices.Services
{
    public interface ICartService
    {
        void Add(Item item);
        void Remove(Item item);
        void Remove(string sku);
        double GetTotal();
        void Clear();
    }
}
