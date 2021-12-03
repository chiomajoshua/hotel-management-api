namespace hotel_management_api_identity.Features.Enquiry.Transaction.Model
{
    public class TransactionResponse
    {
        public int Quantity { get; set; }
        public decimal Paid { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }
}