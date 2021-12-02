﻿using hotel_management_api_identity.Core.Constants;
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
        public Enums.Id IdType { get;}
        public string IdNumber { get;}
        [Required]
        public string Email { get; }
        public Enums.Gender Gender { get;}

        public CreateCustomerRequest(string title, string firstName, string lastName, string phoneNumber, Enums.Id idType, string idNumber, string email, Enums.Gender gender)
        {
            Title = title;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            IdType = idType;
            IdNumber = idNumber;
            Email = email;
            Gender = gender;
        }
    }

    public class CreateCustomerResponse
    {
        public string Email { get; set; }
    }
}