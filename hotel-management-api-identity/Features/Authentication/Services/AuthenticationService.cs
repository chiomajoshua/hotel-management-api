using hotel_management_api_identity.Core.Helpers;
using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Core.Storage.Models;
using hotel_management_api_identity.Core.Storage.QueryRepository;
using hotel_management_api_identity.Features.Authentication.Models;
using hotel_management_api_identity.Features.Enquiry.Employee.Service;
using Newtonsoft.Json;

namespace hotel_management_api_identity.Features.Authentication.Services
{
    public interface IAuthenticationService : IAutoDependencyCore
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        Task<bool> ValidateCredentials(LoginRequest loginRequest);

        /// <summary>
        /// Create logon for user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<string> CreateAccount(string email);

        /// <summary>
        /// Check if User Logon Exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsLogonExists(string email);
    }
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IDapperQuery<Login> _loginQuery;
        private readonly IDapperQuery<Employee> _employeeQuery;
        private readonly IDapperCommand<Login> _loginCommand;
        public AuthenticationService(ILogger<AuthenticationService> logger, IEmployeeService employeeService, IDapperCommand<Login> loginCommand, IDapperQuery<Login> loginQuery,
                                       IDapperQuery<Employee> employeeQuery)
        {
            _logger = logger;
            _employeeService = employeeService;
            _loginCommand = loginCommand;
            _loginQuery = loginQuery;
            _employeeQuery = employeeQuery;
        }

        public async Task<string> CreateAccount(string email)
        {
            try
            {
                _logger.LogInformation(message: $"CreateAccount Request ----->" + email, email);
                if (await _employeeService.IsEmployeeExistsByEmail(email))
                {
                    var defaultPassword = "P@$$w0rd";
                    var employee = await _employeeQuery.GetByDefaultAsync(new Dictionary<string, string>() { { "email", email } });
                    await _loginCommand.AddAsync(new Login { Password = Extensions.Encrypt(defaultPassword), Email = email });
                if (await IsLogonExists(employee.Email)) return defaultPassword;                    
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError("CreateAccount Error" + JsonConvert.SerializeObject(ex), ex.Message);
                return string.Empty;
            }
        }

        public async Task<bool> IsLogonExists(string email)
        {
            try
            {
                var loginResponse = await _loginQuery.GetByDefaultAsync(new Dictionary<string, string>() { { "email", email } });
                if(loginResponse is not null)   return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("IsLogonExists Error", ex.Message);
                return false;
            }
        }

        public async Task<bool> ValidateCredentials(LoginRequest loginRequest)
        {
            try
            {
                _logger.LogInformation($"ValidateCredentials Request -----> {JsonConvert.SerializeObject(loginRequest)}");
                if (await _employeeService.IsEmployeeExistsByEmail(loginRequest.Email))
                {
                    var employeeId = _employeeService.GetEmployeeByEmail(loginRequest.Email).Result.Data.EmployeeId;
                    var loginResponse = await _loginQuery.GetByDefaultAsync(new Dictionary<string, string>() { { "email", loginRequest.Email } });
                    if (loginResponse.Email == loginRequest.Email && Extensions.Decrypt(loginResponse.Password).Equals(loginRequest.Password))
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("ValidateCredentials Error" + JsonConvert.SerializeObject(ex), ex.Message);
                return false;
            }
        }
    }
}