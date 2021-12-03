using hotel_management_api_identity.Core.Constants;

namespace hotel_management_api_identity.Features.Onboarding.Models
{
    public class CreateMenuRequest
    {
        public decimal Price { get; set; }
        public Enums.Category Category { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
    }


    public class CreateMenuResponse
    {
        public string Category { get; set; }
        public string Item { get; set; }
    }
}