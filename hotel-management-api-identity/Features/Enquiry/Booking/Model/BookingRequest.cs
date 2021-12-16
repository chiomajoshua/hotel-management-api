namespace hotel_management_api_identity.Features.Enquiry.Booking.Model
{
    public class BookingRequest
    {

    }

    public class BookingResponse
    {
        public string CustomerEmail { get; set; }
        public string BookingCode { get; set; }
        public bool HasDiscount { get; set; }
        public DateTimeOffset CheckInDate { get; set; }
        public DateTimeOffset CheckOutDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string Room { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ModifiedOn { get; set; } 
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }
}