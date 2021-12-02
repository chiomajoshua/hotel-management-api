using System.ComponentModel.DataAnnotations;

namespace hotel_management_api_identity.Features.Authentication.Models
{
    public class TokenRequest
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
    }

    public class TokenResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTimeOffset ExpiryDate { get; set; } 
    }
}