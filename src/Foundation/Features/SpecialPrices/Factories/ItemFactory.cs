using Foundation.Features.SpecialPrices.Models;

namespace Foundation.Features.SpecialPrices.Factories
{
    public abstract class ItemFactory : IItemFactory
    {
        private Item _item = new Item();

        public Item CreatedItem
        {
            get { return _item; }
        }

        public abstract Item CreateItem(string sku);
    }
}