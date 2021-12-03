using hotel_management_api_identity.Features.Enquiry.Booking.Model;

namespace hotel_management_api_identity.Features.Enquiry.Booking.Config
{
    public static class BookingExtensions
    {
        public static IEnumerable<BookingResponse> ToBookingList(this List<Core.Storage.Models.Booking> bookingData)
        {
            var result = new List<BookingResponse>();
            result.AddRange(bookingData.Select(data => new BookingResponse()
            {
                AmountPaid = data.AmountPaid,
                CheckInDate = data.CheckInDate,
                CheckOutDate = data.CheckOutDate,
                CreatedById = data.CreatedById,
                HasDiscount = data.HasDiscount,
                ModifiedById = data.ModifiedById,
                ModifiedOn = data.ModifiedOn,
                Room = data.Room.Name,
                CreatedOn = data.CreatedOn
            }));

            return result;
        }
        public static BookingResponse ToBooking(this Core.Storage.Models.Booking bookingData)
        {
            return new BookingResponse()
            {
                AmountPaid = bookingData.AmountPaid,
                CheckInDate = bookingData.CheckInDate,
                CheckOutDate = bookingData.CheckOutDate,
                CreatedById = bookingData.CreatedById,
                HasDiscount = bookingData.HasDiscount,
                ModifiedById = bookingData.ModifiedById,
                ModifiedOn = bookingData.ModifiedOn,
                Room = bookingData.Room.Name,
                CreatedOn = bookingData.CreatedOn
            };
        }
    }
}