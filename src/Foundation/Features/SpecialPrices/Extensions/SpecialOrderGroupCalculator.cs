using EPiServer.Commerce.Order;
using EPiServer.Commerce.Order.Calculator;
using Foundation.Features.SpecialPrices.Factories;
using Foundation.Features.SpecialPrices.Services;
using Mediachase.Commerce;
using Mediachase.Commerce.Markets;

namespace Foundation.Features.SpecialPrices.Extensions
{
    public class SpecialOrderGroupCalculator : DefaultOrderGroupCalculator
    {
        private readonly IMarketService _marketService;
        private readonly ICurrentMarket _currentMarketService;
        private readonly IItemFactory _itemFactory;
        private readonly ICartService _cartService;
        private readonly ISpecialCustomerService _specialCustomerService;

        public SpecialOrderGroupCalculator(IOrderFormCalculator orderFormCalculator,
            IReturnOrderFormCalculator returnOrderFormCalculator,
            IMarketService marketService,
            ICurrentMarket currentMarketService,
            IItemFactory itemFactory,
            ICartService cartService,
            ISpecialCustomerService specialCustomerService) : base(orderFormCalculator, returnOrderFormCalculator, marketService)
        {
            _itemFactory = itemFactory;
            _cartService = cartService;
            _marketService = marketService;
            _currentMarketService = currentMarketService;
            _specialCustomerService = specialCustomerService;
        }

        protected override Money CalculateSubTotal(IOrderGroup orderGroup)
        {
            if(_specialCustomerService.IsCurrentCustomerSpecial())
            {
                foreach (var form in orderGroup.Forms)
                {
                    foreach (var lineItem in form.GetAllLineItems())
                    {
                        var createdItem = _itemFactory.CreateItem(lineItem.Code);
                        if (createdItem.Price == null)
                        {
                            createdItem.Price = lineItem.PlacedPrice;
                        }
                        createdItem.Quantity = (int)lineItem.Quantity;
                        _cartService.Add(createdItem);
                    }
                }
                var specialTotal = _cartService.GetTotal();
                _cartService.Clear();
                return new Money(specialTotal, _currentMarketService.GetCurrentMarket().DefaultCurrency);
            }
            else
            {
                return base.CalculateSubTotal(orderGroup);
            }
        }
    }
}