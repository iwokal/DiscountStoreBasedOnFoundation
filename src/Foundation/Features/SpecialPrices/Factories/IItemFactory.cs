using Foundation.Features.SpecialPrices.Models;

namespace Foundation.Features.SpecialPrices.Factories
{
    public interface IItemFactory
    {
         Item CreateItem(string sku);
    }
}