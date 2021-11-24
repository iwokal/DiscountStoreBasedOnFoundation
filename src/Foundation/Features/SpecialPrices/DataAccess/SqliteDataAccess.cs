using Dapper;
using DiscountStore.Areas.DataAccess.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace DiscountStore.Areas.DataAccess
{
    public class SqliteDataAccess
    {
        public static List<PriceModel> LoadPrices()
        {
            using (IDbConnection con = new SqliteConnection(LoadConnectionString()))
            {
                var output = con.Query<PriceModel>("select * from Price", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SavePrice(PriceModel price)
        {
            using (IDbConnection con = new SqliteConnection(LoadConnectionString()))
            {
                con.Execute("insert into Price (SKU, Price) values (@SKU, @Price) ", price);
            }
        }

        public static List<DiscountModel> LoadDiscounts()
        {
            using (IDbConnection con = new SqliteConnection(LoadConnectionString()))
            {
                var output = con.Query<DiscountModel>("select * from Discount", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveDiscount(DiscountModel discount)
        {
            using (IDbConnection con = new SqliteConnection(LoadConnectionString()))
            {
                con.Execute("insert into Discount (SKU, Quantity, Price) values (@SKU, @Quantity, @Price) ", discount);
            }
        }

        public static List<ItemModel> LoadItems()
        {
            using (IDbConnection con = new SqliteConnection(LoadConnectionString()))
            {
                var output = con.Query<ItemModel>("select * from Item", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveItem(ItemModel item)
        {
            using (IDbConnection con = new SqliteConnection(LoadConnectionString()))
            {
                con.Execute("insert into Item (SKU, Name) values (@SKU, @Name) ", item);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}