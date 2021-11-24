using Foundation.Features.SpecialPrices.Models;
using Foundation.Features.SpecialPrices.Services;
using EPiServer.Logging;
using System;

namespace Foundation.Features.SpecialPrices.Factories
{
    public class DiscountItemFactory : IItemFactory
    {
        private IDiscountService _discountService;
        private IPriceService _priceService;
        private static readonly ILogger _log = LogManager.GetLogger(typeof(DiscountItemFactory));

        public DiscountItemFactory(IDiscountService discountService, IPriceService priceService)
        {
            _discountService = discountService;
            _priceService = priceService;
        }

        public Item CreateItem(string sku)
        {
            try
            {
                var newItem = new Item();
                newItem.SKU = sku;
                newItem.Price = _priceService.GetPriceBySku(sku);
                newItem.Discount = _discountService.GetDiscountBySku(sku);
                return newItem;
            }
            catch (Exception ex)
            {
                _log.Error($"Couldn't create item object for SKU({sku}): ", ex);
                return null;
            }
        }
    }
}