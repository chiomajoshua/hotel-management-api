﻿using System.ComponentModel.DataAnnotations;

namespace hotel_management_api_identity.Features.Authentication.Models
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}