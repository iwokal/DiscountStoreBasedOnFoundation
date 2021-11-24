using System.Collections.Generic;

namespace Foundation.Features.SpecialPrices.Models
{
    public class StoreCart
    {
        public StoreCart()
        {
            Items = new Dictionary<string, Item>();
        }

        public Dictionary<string, Item> Items { get; set; }
    }
}