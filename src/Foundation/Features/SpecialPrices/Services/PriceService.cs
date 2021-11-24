using Dapper;
using Foundation.Features.SpecialPrices.DataAccess.Models;
using EPiServer.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Foundation.Features.SpecialPrices.Services
{
    public class PriceService : IPriceService
    {
        private static string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EcfSqlConnection"].ConnectionString;
        private static readonly ILogger _log = LogManager.GetLogger(typeof(PriceService));

        public double GetPriceBySku(string sku)
        {
            try
            {
                var priceEntity = LoadPricesBySku(sku).FirstOrDefault();
                if (priceEntity == null)
                {
                    throw new ArgumentException();
                }
                return priceEntity.Price;
            }
            catch (Exception ex)
            {
                _log.Error($"Error while fetching the price from database for item SKU({sku}): ", ex);
                throw;
            }
        }

        public static List<SpecialPriceModel> LoadPrices()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var output = con.Query<SpecialPriceModel>("select * from [dbo].[SpecialPrice]", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<SpecialPriceModel> LoadPricesBySku(string sku)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var sql = "select * from [dbo].[SpecialPrice] where SKU = @sku";
                var output = con.Query<SpecialPriceModel>(sql, new { sku });
                return output.ToList();
            }
        }
    }
}
