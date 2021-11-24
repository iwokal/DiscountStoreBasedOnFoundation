using Foundation.Features.SpecialPrices.Models;
using EPiServer.Logging;
using System;

namespace Foundation.Features.SpecialPrices.Services
{
    public class CartService : ICartService
    {
        public StoreCart CurrentCart => _cart;
        //here we would fetch the current cart for the user properly using injected user service 
        private static StoreCart _cart = new StoreCart();
        private static readonly ILogger _log = LogManager.GetLogger(typeof(CartService));

        public void Add(Item item)
        {
            if (_cart.Items.ContainsKey(item.SKU))
            {
                _cart.Items[item.SKU].Quantity += item.Quantity;
            }
            else
            {
                _cart.Items.Add(item.SKU, item);
            }
        }

        public void Remove(Item item)
        {
            Remove(item.SKU);
        }

        public void Remove(string sku)
        {
            _cart.Items.Remove(sku);
        }

        public decimal GetTotal()
        {
            try
            {
                decimal total = 0;
                foreach (var item in _cart.Items)
                {
                    var quantity = item.Value.Quantity;
                    var discount = item.Value.Discount;
                    decimal price;
                    if (discount != null)
                    {
                        price = Math.Round((quantity / discount.Quantity * discount.Price) + (decimal)((quantity % discount.Quantity) * item.Value.Price), 2);
                    }
                    else
                    {
                        price = Math.Round((decimal)(quantity * item.Value.Price), 2);
                    }
                    total += price;
                }
                return Math.Round(total, 2);
            }
            catch(Exception ex)
            {
                _log.Error("Error while calculating cart total: ", ex);
                throw;
            }
        }

        public void Clear()
        {
            _cart.Items.Clear();
        }
    }
}
