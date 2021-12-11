using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers.Extension;
using System.ComponentModel.DataAnnotations;

namespace hotel_management_api_identity.Features.Onboarding.Models
{
    public class CreateCustomerRequest
    {
        [Required]
        public string Title { get; }
        [Required]
        public string FirstName { get; }
        [Required]
        public string LastName { get;}
        [Required]
        public string PhoneNumber { get; }
        public string IdType { get;}
        public string IdNumber { get;}
        [Required]
        public string Email { get; }
        public string Gender { get;}
        public string CustomerCode { get; }

        public CreateCustomerRequest(string title, string firstName, string lastName, string phoneNumber, string idType, string idNumber, string email, string gender)
        {
            Title = title;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            IdType = idType;
            IdNumber = idNumber;
            Email = email;
            Gender = gender;
            CustomerCode = Extensions.RandomCustomerCode();
        }
    }

    public class CreateCustomerResponse
    {
        public string Email { get; set; }
    }
}