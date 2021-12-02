using System.ComponentModel.DataAnnotations;

namespace hotel_management_api_identity.Features.Enquiry.Customer.Model
{
    public class CustomerRequest
    {
        [Required]
        public string Email { get; set; }
    }

    public class CustomerResponse
    {        
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string IdType { get; set; }
        public string IdNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}