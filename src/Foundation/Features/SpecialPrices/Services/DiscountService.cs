using Foundation.Features.SpecialPrices.Models;
using Foundation.Features.SpecialPrices.DataAccess.Models;
using System.Linq;
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Foundation.Features.SpecialPrices.Services
{
    public class DiscountService : IDiscountService
    {
        private static string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EcfSqlConnection"].ConnectionString;
        public Discount GetDiscountBySku(string sku)
        {
            var discountFromDB = LoadDiscountsBySku(sku).FirstOrDefault();
            if (discountFromDB != null)
            {
                var discount = new Discount();
                discount.Quantity = discountFromDB.Quantity;
                discount.Price = discountFromDB.Price;
                return discount;
            }
            return null;
        }

        public bool TryGetDiscountBySku(string sku, out Discount discount)
        {
            discount = new Discount();
            var discountFromDB = LoadDiscountsBySku(sku).FirstOrDefault();
            if (discountFromDB != null)
            {
                discount.Quantity = discountFromDB.Quantity;
                discount.Price = discountFromDB.Price;
                return false;
            }
            return true;
        }

        private List<SpecialDiscountModel> LoadDiscounts()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var output = con.Query<SpecialDiscountModel>("select * from [dbo].[SpecialDiscount]", new DynamicParameters());
                return output.ToList();
            }
        }

        private List<SpecialDiscountModel> LoadDiscountsBySku(string sku)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var sql = "select * from [dbo].[SpecialDiscount] where SKU = @sku";
                var output = con.Query<SpecialDiscountModel>(sql, new { sku });
                return output.ToList();
            }
        }
    }
}
