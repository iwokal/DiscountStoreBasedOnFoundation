using EPiServer.Commerce.Order;
using EPiServer.Commerce.Order.Calculator;
using Foundation.Features.SpecialPrices.Factories;
using Mediachase.Commerce;
using Mediachase.Commerce.Markets;

namespace Foundation.Features.SpecialPrices.Extensions
{
    public class SpecialOrderGroupCalculator : DefaultOrderGroupCalculator
    {
        private readonly IMarketService _marketService;
        private readonly IItemFactory _itemFactory;

        public SpecialOrderGroupCalculator(IOrderFormCalculator orderFormCalculator,
            IReturnOrderFormCalculator returnOrderFormCalculator,
            IMarketService marketService,
            IItemFactory itemFactory) : base(orderFormCalculator, returnOrderFormCalculator, marketService)
        {
            _itemFactory = itemFactory;
        }

        public override Money CalculateSubTotal(IOrderGroup orderGroup)
        {
            foreach (var form in orderGroup.Forms)
            {
                foreach (var lineItem in form.GetAllLineItems())
                {
                    lineItem.PlacedPrice;
                }
            }
        }
    }
}