using Dapper;
using EPiServer.Logging;
using Foundation.Commerce.Customer;
using Foundation.Commerce.Customer.Services;
using Foundation.Features.SpecialPrices.DataAccess.Models;
using Foundation.Features.SpecialPrices.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Foundation.Features.SpecialPrices.Services
{
    public class SpecialCustomerService : ISpecialCustomerService
    {
        private static string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EcfSqlConnection"].ConnectionString;
        private static readonly ILogger _log = LogManager.GetLogger(typeof(PriceService));
        private readonly ICustomerService _customerService;
        public SpecialCustomerService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public bool IsCurrentCustomerSpecial()
        {
            return IsSpecialCustomer(_customerService.GetCurrentContact());
        }

        public bool IsSpecialCustomer(FoundationContact contact)
        {
            try
            {
                if (contact == null || contact.UserId == null)
                {
                    return false;
                }
                return GetSpecialCustomerByUserId(contact.UserId).Any();
            }
            catch(Exception ex)
            {
                _log.Error("Error while checking if user is super user", ex);
                return false;
            }
        }

        private List<SpecialCustomerModel> GetSpecialCustomers()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var sql = "select * from [dbo].[SpecialCustomer]";
                var output = con.Query<SpecialCustomerModel>(sql);
                return output.ToList();
            }
        }

        private List<SpecialCustomerModel> GetSpecialCustomerByUserId(string userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var sql = "select * from [dbo].[SpecialCustomer] where UserId = @userId";
                var output = con.Query<SpecialCustomerModel>(sql, new { userId });
                return output.ToList();
            }
        }
    }
}
