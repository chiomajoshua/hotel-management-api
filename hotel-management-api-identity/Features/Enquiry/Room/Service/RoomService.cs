using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers;
using hotel_management_api_identity.Core.Storage.QueryRepository;
using hotel_management_api_identity.Features.Enquiry.Room.Config;
using hotel_management_api_identity.Features.Enquiry.Room.Model;

namespace hotel_management_api_identity.Features.Enquiry.Room.Service
{
    public interface IRoomService : IAutoDependencyCore
    {
        /// <summary>
        /// Fetches Room By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<GenericResponse<RoomResponse>> GetRoomByName(string name);

        /// <summary>
        /// Get All Rooms
        /// </summary>
        /// <returns></returns>
        Task<GenericResponse<IEnumerable<RoomResponse>>> GetRooms(int pageSize, int pageNumber);

        /// <summary>
        /// Check if Room Exists Before Creating
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> IsRoomExists(string name);


        /// <summary>
        /// Checks if room is currently in use
        /// </summary>
        /// <param name="checkInDate"></param>
        /// <param name="checkOutDate"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        Task<bool> IsRoomTaken(DateTimeOffset checkInDate, DateTimeOffset checkOutDate, Guid roomId);
    }



    public class RoomService : IRoomService
    {
        private readonly ILogger<RoomService> _logger;
        private readonly IDapperQuery<Core.Storage.Models.Room> _roomQuery;
        private readonly IDapperQuery<Core.Storage.Models.Booking> _bookingQuery;
        public RoomService(IDapperQuery<Core.Storage.Models.Room> roomQuery, ILogger<RoomService> logger, IDapperQuery<Core.Storage.Models.Booking> bookingQuery)
        {
            _logger = logger;
            _roomQuery = roomQuery;
            _bookingQuery = bookingQuery;
        }


        public async Task<GenericResponse<RoomResponse>> GetRoomByName(string name)
        {
            try
            {
                var query = new Dictionary<string, string>() { { "name", name } };
                var result = await _roomQuery.GetByDefaultAsync(query);
                if (result is not null) return new GenericResponse<RoomResponse> { Data = result.ToRoom(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<RoomResponse> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GenericResponse<RoomResponse> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }

        public async Task<GenericResponse<IEnumerable<RoomResponse>>> GetRooms(int pageSize, int pageNumber)
        {
            try
            {
                var result = await _roomQuery.GetByAsync(pageSize, pageNumber);
                if (result is not null) return new GenericResponse<IEnumerable<RoomResponse>> { Data = result.ToList().ToRoomList(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<IEnumerable<RoomResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GenericResponse<IEnumerable<RoomResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }

        public async Task<bool> IsRoomExists(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name)) return false;
                var query = new Dictionary<string, string>() { { "Name", name } };
                var result = await _roomQuery.IsExistAsync(query);
                if (result) return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> IsRoomTaken(DateTimeOffset checkInDate, DateTimeOffset checkOutDate, Guid roomId)
        {
            try
            {
                if (string.IsNullOrEmpty(roomId.ToString())) return false;
                var query = new Dictionary<string, DateTimeOffset>() { { "CheckInDate", checkInDate }, { "CheckOutDate", checkOutDate } };
                var result = await _bookingQuery.IsExistAsync(query, roomId);
                if (result) return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }        
    }
}
