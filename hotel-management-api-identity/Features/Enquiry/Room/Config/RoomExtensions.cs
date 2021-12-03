using hotel_management_api_identity.Features.Enquiry.Room.Model;
using hotel_management_api_identity.Features.Onboarding.Models;

namespace hotel_management_api_identity.Features.Enquiry.Room.Config
{
    public static class RoomExtensions
    {
        public static Core.Storage.Models.Room ToDbRoom(this CreateRoomRequest createRoomRequest)
        {
            return new Core.Storage.Models.Room()
            {
               Name = createRoomRequest.Name,
               Price = createRoomRequest.Price
            };
        }

        public static IEnumerable<RoomResponse> ToRoomList(this List<Core.Storage.Models.Room> roomData)
        {
            var result = new List<RoomResponse>();

            result.AddRange(roomData.Select(data => new RoomResponse()
            {
                 Price = data.Price,
                 Name = data.Name
            }));

            return result;

        }
        public static RoomResponse ToRoom(this Core.Storage.Models.Room roomData)
        {
            return new RoomResponse()
            {
                 Name = roomData.Name,
                 Price = roomData.Price
            };
        }
    }
}