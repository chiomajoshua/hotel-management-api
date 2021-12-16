using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers;
using hotel_management_api_identity.Core.Storage.QueryRepository;
using hotel_management_api_identity.Features.Enquiry.Booking.Config;
using hotel_management_api_identity.Features.Enquiry.Booking.Model;

namespace hotel_management_api_identity.Features.Enquiry.Booking.Service
{
    public interface IBookingService : IAutoDependencyCore
    {
        Task<GenericResponse<IEnumerable<BookingResponse>>> GetAllBookings(int pageSize, int pageNumber);

        Task<GenericResponse<IEnumerable<BookingResponse>>> GetBookingsByEmail(string email);
    }
    public class BookingService : IBookingService
    {
        private readonly ILogger<BookingService> _logger;
        private readonly IDapperQuery<Core.Storage.Models.Booking> _bookingQuery;
        public BookingService(ILogger<BookingService> logger, IDapperQuery<Core.Storage.Models.Booking> bookingQuery)
        {
            _bookingQuery = bookingQuery;
            _logger = logger;
        }

        public async Task<GenericResponse<IEnumerable<BookingResponse>>> GetAllBookings(int pageSize, int pageNumber)
        {
            try
            {
                var result = await _bookingQuery.GetByAsync(pageSize, pageNumber);
                if (result is not null) return new GenericResponse<IEnumerable<BookingResponse>> { Data = result.ToList().ToBookingList(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<IEnumerable<BookingResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllBookings Error", ex.Message);
                return new GenericResponse<IEnumerable<BookingResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }

        public async Task<GenericResponse<IEnumerable<BookingResponse>>> GetBookingsByEmail(string email)
        {
            try
            {
                var query = new Dictionary<string, string>() { { "CustomerEmail", email } };
                var result = await _bookingQuery.GetByAsync(query);
                if (result is not null) return new GenericResponse<IEnumerable<BookingResponse>> { Data = result.ToList().ToBookingList(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<IEnumerable<BookingResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllBookings Error", ex.Message);
                return new GenericResponse<IEnumerable<BookingResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }
    }
}