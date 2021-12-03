namespace hotel_management_api_identity.Features.Enquiry.Room.Model
{
    public class RoomRequest
    {
        public string Name { get; set; }
    }

    public class RoomResponse
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}