using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers;
using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Core.Storage.QueryRepository;
using hotel_management_api_identity.Features.Enquiry.Customer.Config;
using hotel_management_api_identity.Features.Enquiry.Customer.Model;

namespace hotel_management_api_identity.Features.Enquiry.Customer.Service
{
    public interface ICustomerService : IAutoDependencyCore
    {
        /// <summary>
        /// Fetches Customer By Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<GenericResponse<CustomerResponse>> GetCustomerByEmail(string email);

        /// <summary>
        /// Checks If Customer Exists Using Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsCustomerExistsByEmail(string email);

        /// <summary>
        /// Checks If Customer Exists Using Phone
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<bool> IsCustomerExistsByPhone(string phone);

        /// <summary>
        /// Fetches Customer By Phone
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<GenericResponse<CustomerResponse>> GetCustomerByPhone(string phone);


       /// <summary>
       /// Gets All Customers
       /// </summary>
       /// <param name="pageSize"></param>
       /// <param name="pageNumber"></param>
       /// <returns></returns>
        Task<GenericResponse<IEnumerable<CustomerResponse>>> GetCustomers(int pageSize, int pageNumber);
    }


    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;        
        private readonly IDapperQuery<Core.Storage.Models.Customer> _customerQuery;

        public CustomerService(IDapperQuery<Core.Storage.Models.Customer> customerQuery,
                               ILogger<CustomerService> logger)
        {            
            _customerQuery = customerQuery;
            _logger = logger;
        }

        

        public async Task<GenericResponse<CustomerResponse>> GetCustomerByEmail(string email)
        {           
            try 
            {
                var query = new Dictionary<string, string>() { { "email", email } };
                var result = await _customerQuery.GetByDefaultAsync(query);
                if (result is not null) return new GenericResponse<CustomerResponse> { Data = result.ToCustomer(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<CustomerResponse> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError("GetCustomerByEmail Error", ex.Message);
                return new GenericResponse<CustomerResponse> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }

        public async Task<GenericResponse<CustomerResponse>> GetCustomerByPhone(string phone)
        {
            try
            {
                var query = new Dictionary<string, string>() { { "phoneNumber", phone } };
                var result = await _customerQuery.GetByDefaultAsync(query);
                if (result is not null) return new GenericResponse<CustomerResponse> { Data = result.ToCustomer(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<CustomerResponse> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError("GetCustomerByPhone Error", ex.Message);
                return new GenericResponse<CustomerResponse> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }

        public async Task<GenericResponse<IEnumerable<CustomerResponse>>> GetCustomers(int pageSize, int pageNumber)
        {
            try
            {
                var result = await _customerQuery.GetByAsync(pageSize, pageNumber);
                if (result.Any()) return new GenericResponse<IEnumerable<CustomerResponse>> { Data = result.ToList().ToCustomerList(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<IEnumerable<CustomerResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError("GetCustomers Error", ex.Message);
                return new GenericResponse<IEnumerable<CustomerResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }

        public async Task<bool> IsCustomerExistsByEmail(string email)
        {
            try
            {               
                if(string.IsNullOrEmpty(email)) return false;
                if (!Extensions.IsValidEmail(email)) return false;
                var query = new Dictionary<string, string>() { { "Email", email } };
                var result = await _customerQuery.IsExistAsync(query);
                if (result) return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("IsCustomerExistsByEmail Error", ex.Message);
                return false;
            }
        }

        public async Task<bool> IsCustomerExistsByPhone(string phone)
        {
            try
            {
                if(string.IsNullOrEmpty(phone)) return false;
                if (!Extensions.IsValidPhoneNumber(phone)) return false;
                var query = new Dictionary<string, string>() { { "PhoneNumber", phone } };
                var result = await _customerQuery.IsExistAsync(query);
                if (result) return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("IsCustomerExistsByPhone Error", ex.Message);
                return false;
            }
        }
    }
}