using hotel_management_api_identity.Core.Constants;

namespace hotel_management_api_identity.Features.Enquiry.Transaction.Model
{
    public class TransactionResponse
    {       
        public string OrderCode { get; set; }
        public decimal Total { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }

    public class TransactionDetailResponse
    {
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }
    }
}