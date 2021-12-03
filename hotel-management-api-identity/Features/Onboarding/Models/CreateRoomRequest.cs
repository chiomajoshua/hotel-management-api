namespace hotel_management_api_identity.Features.Onboarding.Models
{
    public class CreateRoomResponse
    {
        public string Name { get; set; }
    }
    public class CreateRoomRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}