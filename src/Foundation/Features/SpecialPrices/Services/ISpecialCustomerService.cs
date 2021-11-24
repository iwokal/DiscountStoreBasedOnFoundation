using Foundation.Commerce.Customer;

namespace Foundation.Features.SpecialPrices.Services
{
    public interface ISpecialCustomerService
    {
        bool IsCurrentCustomerSpecial();

        bool IsSpecialCustomer(FoundationContact contact);
    }
}
