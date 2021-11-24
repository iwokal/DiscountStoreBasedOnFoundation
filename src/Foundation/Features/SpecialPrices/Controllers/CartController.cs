using DiscountStore.Areas.Cart.Factories;
using DiscountStore.Areas.Cart.Services;
using log4net;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace DiscountStore.Areas.Cart.Controllers
{
    public class CartController : ApiController
    {
        private ICartService _cartService;
        private DiscountItemFactory _itemFactory;
        private static readonly ILog _log = LogManager.GetLogger(typeof(CartController));

        public CartController(ICartService cartService, DiscountItemFactory itemFactory)
        {
            _cartService = cartService;
            _itemFactory = itemFactory;
        }

        // POST: Cart/Add/string
        public ActionResult Add(string sku)
        {
            try
            {
                var item = _itemFactory.CreateItem(sku);
                if (item == null)
                {
                    _log.Debug($"Item with this SKU ({sku}) doesn't exist");
                    return new HttpStatusCodeResult(HttpStatusCode.NoContent, "Item with this SKU doesn't exist");
                }
                _cartService.Add(item);
                return new HttpStatusCodeResult(HttpStatusCode.OK, "Item added to cart");
            }
            catch (Exception ex)
            {
                _log.Error($"Error while fetching item (SKU- {sku}) from database: ", ex);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error while fetching item from database");
            }
        }

        // POST: Cart/Remove/string
        public ActionResult Remove(string sku)
        {
            try
            {
                _cartService.Remove(sku);
                return new HttpStatusCodeResult(HttpStatusCode.OK, "Item removed from cart");
            }
            catch (Exception ex)
            {
                _log.Error($"Error while removing item (SKU- {sku})  from cart: ", ex);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error while removing item from cart");
            }
        }

        // GET: Cart/GetTotal
        public double GetTotal()
        {
            try
            {
                return _cartService.GetTotal();
            }
            catch (Exception ex)
            {
                _log.Error($"Error while getting cart total", ex);
                throw ex;
            }
        }

        // POST: Cart/Clear
        public ActionResult Clear()
        {
            try
            {
                _cartService.Clear();
                return new HttpStatusCodeResult(HttpStatusCode.OK, "All items removed from cart");
            }
            catch (Exception ex)
            {
                _log.Error("Error while clearing the cart: ", ex);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error while clearing the cart");
            }
        }
    }
}