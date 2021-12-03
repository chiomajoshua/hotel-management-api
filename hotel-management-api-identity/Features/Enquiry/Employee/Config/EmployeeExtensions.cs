using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Features.Enquiry.Employee.Model;
using hotel_management_api_identity.Features.Onboarding.Models;

namespace hotel_management_api_identity.Features.Enquiry.Employee.Config
{
    public static class EmployeeExtensions
    {
        public static IEnumerable<EmployeeResponse> ToEmployeeList(this List<Core.Storage.Models.Employee> employeeData)
        {
            var result = new List<EmployeeResponse>();
            result.AddRange(employeeData.Select(data => new EmployeeResponse()
            {
                EmployeeId = data.Id,
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Gender = data.Gender.Description(),
                PhoneNumber = data.PhoneNumber,
                CreatedOn = data.CreatedOn,
                Address = data.Address
            }));

            return result;

        }
        public static EmployeeResponse ToEmployee(this Core.Storage.Models.Employee employeeData)
        {
            return new EmployeeResponse()
            {
                EmployeeId = employeeData.Id,
                Email = employeeData.Email,
                FirstName = employeeData.FirstName,
                LastName = employeeData.LastName,
                Gender = employeeData.Gender.Description(),
                PhoneNumber = employeeData.PhoneNumber,
                CreatedOn = employeeData.CreatedOn,
                Address = employeeData.Address
            };
        }

        public static Core.Storage.Models.Employee ToDbEmployee(this CreateEmployeeRequest createEmployeeRequest)
        {
            return new Core.Storage.Models.Employee()
            {
                Email = createEmployeeRequest.Email,
                FirstName = createEmployeeRequest.FirstName,
                LastName = createEmployeeRequest.LastName,
                PhoneNumber = createEmployeeRequest.PhoneNumber,
                Gender = createEmployeeRequest.Gender,
                Address = createEmployeeRequest.Address,
                UserType = createEmployeeRequest.UserType
            };
        }
    }
}