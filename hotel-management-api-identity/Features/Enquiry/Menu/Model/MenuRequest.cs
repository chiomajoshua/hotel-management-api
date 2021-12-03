namespace hotel_management_api_identity.Features.Enquiry.Menu.Model
{
    public class MenuRequest
    {
        public string Item { get; set; }
    }

    public class MenuResponse
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }
}