using hotel_management_api_identity.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace hotel_management_api_identity.Features.Onboarding.Models
{
    public class CreateEmployeeRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Enums.Gender Gender { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public Enums.User UserType { get; set; }

        public CreateEmployeeRequest( string firstName, string lastName, string phoneNumber, string idNumber, string email, Enums.Gender gender, string address, Enums.User userType)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            UserType = userType;
            Address = address;
            Email = email;
            Gender = gender;
        }
    }


    public class CreateEmployeeResponse
    {
        public string EmployeeCode { get; set; }
        public string Email { get; set;}
        public string Password { get; set; }
    }
}