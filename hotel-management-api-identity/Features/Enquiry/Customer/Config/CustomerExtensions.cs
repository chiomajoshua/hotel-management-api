using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Features.Enquiry.Customer.Model;
using hotel_management_api_identity.Features.Onboarding.Models;

namespace hotel_management_api_identity.Features.Enquiry.Customer.Config
{
    public static class CustomerExtensions
    {
        public static IEnumerable<CustomerResponse> ToCustomerList(this List<Core.Storage.Models.Customer> customerData)
        {
            new List<CustomerResponse>().AddRange(customerData.Select(data => new CustomerResponse()
            {
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                IdNumber = data.IdNumber,
                Gender = data.Gender.Description(),
                IdType = data.IdType.Description(),
                PhoneNumber = data.PhoneNumber,
                Title = data.Title,
                CreatedOn = data.CreatedOn
            }));

            return new List<CustomerResponse>();            
        }
        public static CustomerResponse ToCustomer(this Core.Storage.Models.Customer customerData)
        {
            return new CustomerResponse()
            {
                Email = customerData.Email,
                FirstName = customerData.FirstName,
                LastName = customerData.LastName,
                IdNumber = customerData.IdNumber,
                Gender = customerData.Gender.Description(),
                IdType = customerData.IdType.Description(),
                PhoneNumber = customerData.PhoneNumber,
                Title = customerData.Title,
                CreatedOn = customerData.CreatedOn
            };
        }

        public static Core.Storage.Models.Customer ToDbCustomer(this CreateCustomerRequest createCustomerRequest)
        {
            return new Core.Storage.Models.Customer
            {
                Email = createCustomerRequest.Email,
                FirstName = createCustomerRequest.FirstName,
                LastName = createCustomerRequest.LastName,
                PhoneNumber = createCustomerRequest.PhoneNumber,               
                Gender = createCustomerRequest.Gender,
                IdNumber = createCustomerRequest.IdNumber,
                Title = createCustomerRequest.Title,
                IdType = createCustomerRequest.IdType
            };
        }
    }
}