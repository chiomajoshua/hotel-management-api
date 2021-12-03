using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers;
using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Core.Storage.QueryRepository;
using hotel_management_api_identity.Features.Enquiry.Employee.Config;
using hotel_management_api_identity.Features.Enquiry.Employee.Model;

namespace hotel_management_api_identity.Features.Enquiry.Employee.Service
{
    public interface IEmployeeService : IAutoDependencyCore
    {
        /// <summary>
        /// Fetches Employee By Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<GenericResponse<EmployeeResponse>> GetEmployeeByEmail(string email);

        /// <summary>
        /// Checks If Employee Exists Using Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsEmployeeExistsByEmail(string email);

        /// <summary>
        /// Checks If Employee Exists Using Phone
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<bool> IsEmployeeExistsByPhone(string phone);

        /// <summary>
        /// Fetches Employee By Phone
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<GenericResponse<EmployeeResponse>> GetEmployeeByPhone(string phone);

        /// <summary>
        /// Gets All Employees
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Task<GenericResponse<IEnumerable<EmployeeResponse>>> GetEmployees(int pageSize, int pageNumber);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly ILogger<EmployeeService> _logger;
        private readonly IDapperQuery<Core.Storage.Models.Employee> _employeeQuery;

        public EmployeeService(IDapperQuery<Core.Storage.Models.Employee> employeeQuery, ILogger<EmployeeService> logger)
        {
            _employeeQuery = employeeQuery;
            _logger = logger;
        }

        public async Task<GenericResponse<EmployeeResponse>> GetEmployeeByEmail(string email)
        {
            try
            {
                var query = new Dictionary<string, string>() { { "email", email } };
                var result = await _employeeQuery.GetByDefaultAsync(query);
                if (result is not null) return new GenericResponse<EmployeeResponse> { Data = result.ToEmployee(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<EmployeeResponse> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GenericResponse<EmployeeResponse> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }

        public async Task<GenericResponse<EmployeeResponse>> GetEmployeeByPhone(string phone)
        {
            try
            {
                var query = new Dictionary<string, string>() { { "phoneNumber", phone } };
                var result = await _employeeQuery.GetByDefaultAsync(query);
                if (result is not null) return new GenericResponse<EmployeeResponse> { Data = result.ToEmployee(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<EmployeeResponse> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GenericResponse<EmployeeResponse> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }

        public async Task<GenericResponse<IEnumerable<EmployeeResponse>>> GetEmployees(int pageSize, int pageNumber)
        {
            try
            {
                var result = await _employeeQuery.GetByAsync(pageSize, pageNumber);
                if (result.Any()) return new GenericResponse<IEnumerable<EmployeeResponse>> { Data = result.ToList().ToEmployeeList(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<IEnumerable<EmployeeResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GenericResponse<IEnumerable<EmployeeResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }

        public async Task<bool> IsEmployeeExistsByEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email)) return false;
                if (!Extensions.IsValidEmail(email)) return false;
                var query = new Dictionary<string, string>() { { "Email", email } };
                var result = await _employeeQuery.IsExistAsync(query);
                if (result) return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> IsEmployeeExistsByPhone(string phone)
        {
            try
            {
                if (string.IsNullOrEmpty(phone)) return false;
                if (!Extensions.IsValidPhoneNumber(phone)) return false;
                var query = new Dictionary<string, string>() { { "PhoneNumber", phone } };
                var result = await _employeeQuery.IsExistAsync(query);
                if (result) return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}