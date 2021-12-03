namespace hotel_management_api_identity.Core.Constants
{
    public class GenericRequest
    {
        public int PageSize { get; set; } = 0;
        public int PageNumber { get; set; } = 0;
    }

    public class GetByDateRangeRequest
    {
        public DateTimeOffset StartDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset EndDate { get; set; } = DateTimeOffset.UtcNow;
    }
}