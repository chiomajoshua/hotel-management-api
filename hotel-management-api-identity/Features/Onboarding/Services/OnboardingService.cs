using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers;
using hotel_management_api_identity.Core.Storage.QueryRepository;
using hotel_management_api_identity.Features.Authentication.Services;
using hotel_management_api_identity.Features.Enquiry.Customer.Config;
using hotel_management_api_identity.Features.Enquiry.Customer.Service;
using hotel_management_api_identity.Features.Enquiry.Employee.Config;
using hotel_management_api_identity.Features.Enquiry.Employee.Service;
using hotel_management_api_identity.Features.Enquiry.Room.Config;
using hotel_management_api_identity.Features.Enquiry.Room.Service;
using hotel_management_api_identity.Features.Onboarding.Models;
using Newtonsoft.Json;

namespace hotel_management_api_identity.Features.Onboarding.Services
{
    public interface IOnboardingService : IAutoDependencyCore
    {
        /// <summary>
        /// Creates a New Customer Profile
        /// </summary>
        /// <param name="createCustomerRequest"></param>
        /// <returns></returns>
        Task<GenericResponse<CreateCustomerResponse>> CreateCustomer(CreateCustomerRequest createCustomerRequest);

        /// <summary>
        /// Creates a New Employee Profile
        /// </summary>
        /// <param name="createEmployeeRequest"></param>
        /// <returns></returns>
        Task<GenericResponse<CreateEmployeeResponse>> CreateEmployee(CreateEmployeeRequest createEmployeeRequest);


        /// <summary>
        /// Add New Room
        /// </summary>
        /// <param name="createRoomRequest"></param>
        /// <returns></returns>
        Task<GenericResponse<CreateRoomResponse>> CreateRoom(CreateRoomRequest createRoomRequest);
    }


    public class OnboardingService : IOnboardingService
    {
        private readonly IDapperCommand<Core.Storage.Models.Customer> _customerCommand;
        private readonly IDapperCommand<Core.Storage.Models.Employee> _employeeCommand;
        private readonly IDapperCommand<Core.Storage.Models.Room> _roomCommand;
        private readonly ICustomerService _customerService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IRoomService _roomService;
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<OnboardingService> _logger;
        public OnboardingService(IDapperCommand<Core.Storage.Models.Customer> customerCommand,ICustomerService customerService, ILogger<OnboardingService> logger,
                                  IDapperCommand<Core.Storage.Models.Employee> employeeCommand, IEmployeeService employeeService, IAuthenticationService authenticationService,
                                  IRoomService roomService, IDapperCommand<Core.Storage.Models.Room> roomCommand)
        {
            _customerService = customerService;
            _employeeService = employeeService;
            _roomService = roomService;
            _customerCommand = customerCommand;
            _employeeCommand = employeeCommand;
            _logger = logger;
            _authenticationService = authenticationService;
            _roomCommand = roomCommand;
        }

        public async Task<GenericResponse<CreateCustomerResponse>> CreateCustomer(CreateCustomerRequest createCustomerRequest)
        {
            try
            {
                _logger.LogInformation($"Create Customer Request ----->>>> {JsonConvert.SerializeObject(createCustomerRequest)}");
                var isCustomerExists = await _customerService.IsCustomerExistsByEmail(createCustomerRequest.Email) || await _customerService.IsCustomerExistsByPhone(createCustomerRequest.PhoneNumber);
                if (!isCustomerExists)
                {
                    await _customerCommand.AddAsync(createCustomerRequest.ToDbCustomer());
                    if (await _customerService.IsCustomerExistsByEmail(createCustomerRequest.Email))
                        return new GenericResponse<CreateCustomerResponse> { IsSuccessful = true, Message = ResponseMessages.CustomerCreatedSuccessfully, Data = new CreateCustomerResponse { Email = createCustomerRequest.Email } };
                    return new GenericResponse<CreateCustomerResponse> { IsSuccessful = false, Message = ResponseMessages.CustomerCreationFailed };
                }
                return new GenericResponse<CreateCustomerResponse> { IsSuccessful = false, Message = ResponseMessages.CustomerAlreadyExists };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GenericResponse<CreateCustomerResponse> { IsSuccessful = false, Message = ResponseMessages.GeneralError };
            }
        }

        public async Task<GenericResponse<CreateEmployeeResponse>> CreateEmployee(CreateEmployeeRequest createEmployeeRequest)
        {
            try
            {
                _logger.LogInformation($"Create Employee Request ----->>>> {JsonConvert.SerializeObject(createEmployeeRequest)}");
                var isCustomerExists = await _employeeService.IsEmployeeExistsByEmail(createEmployeeRequest.Email) || await _employeeService.IsEmployeeExistsByPhone(createEmployeeRequest.PhoneNumber);
                if (!isCustomerExists)
                {
                    await _employeeCommand.AddAsync(createEmployeeRequest.ToDbEmployee());
                    if (await _employeeService.IsEmployeeExistsByEmail(createEmployeeRequest.Email))
                    {
                        var createAccountResponse = await _authenticationService.CreateAccount(createEmployeeRequest.Email);
                        if(!string.IsNullOrEmpty(createAccountResponse))
                        return new GenericResponse<CreateEmployeeResponse> { IsSuccessful = true, Message = ResponseMessages.EmployeeCreatedSuccessfully, Data = new CreateEmployeeResponse { Email = createEmployeeRequest.Email, Password =  createAccountResponse} };
                    }
                    return new GenericResponse<CreateEmployeeResponse> { IsSuccessful = false, Message = ResponseMessages.EmployeeCreationFailed };
                }
                return new GenericResponse<CreateEmployeeResponse> { IsSuccessful = false, Message = ResponseMessages.EmployeeAlreadyExists };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GenericResponse<CreateEmployeeResponse> { IsSuccessful = false, Message = ResponseMessages.GeneralError };
            }
        }

        public async Task<GenericResponse<CreateRoomResponse>> CreateRoom(CreateRoomRequest createRoomRequest)
        {
            try
            {
                _logger.LogInformation($"Create Room Request ----->>>> {JsonConvert.SerializeObject(createRoomRequest)}");
                var isRoomExists = await _roomService.IsRoomExists(createRoomRequest.Name);
                if (!isRoomExists)
                {
                    await _roomCommand.AddAsync(createRoomRequest.ToDbRoom());
                    if (await _roomService.IsRoomExists(createRoomRequest.Name))
                            return new GenericResponse<CreateRoomResponse> { IsSuccessful = true, Message = ResponseMessages.RoomCreatedSuccessfully, Data = new CreateRoomResponse { Name = createRoomRequest.Name} };
                    
                    return new GenericResponse<CreateRoomResponse> { IsSuccessful = false, Message = ResponseMessages.RoomCreationFailed };
                }
                return new GenericResponse<CreateRoomResponse> { IsSuccessful = false, Message = ResponseMessages.RoomAlreadyExists };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GenericResponse<CreateRoomResponse> { IsSuccessful = false, Message = ResponseMessages.GeneralError };
            }
        }
    }
}