namespace hotel_management_api_identity.Core.Constants
{
    public class BaseResponse
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
        public string? StatusCode { get; set; }
    }
}