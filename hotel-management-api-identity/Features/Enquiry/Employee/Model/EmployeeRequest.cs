using hotel_management_api_identity.Features.Enquiry.Customer.Model;

namespace hotel_management_api_identity.Features.Enquiry.Employee.Model
{
    public class EmployeeRequest : CustomerRequest
    {
    }

    public class EmployeeResponse
    {
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}