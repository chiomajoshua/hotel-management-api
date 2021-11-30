using System.ComponentModel.DataAnnotations;

namespace hotel_management_api_identity.Features.Authentication.Models
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}