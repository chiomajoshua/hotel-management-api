namespace hotel_management_api_identity.Features.Transaction.Models
{
    public class CreateBookingRequest
    {
        public string Room { get; set; }
        public string CustomerEmail { get; set; }
        public bool HasDiscount { get; set; }
        public DateTimeOffset CheckInDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset CheckOutDate { get; set; }
        public decimal AmountPaid { get; set; }
    }


    public class CreateBookingResponse
    {
        public bool IsSuccess { get; set; }
    }
}