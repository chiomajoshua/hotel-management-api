using hotel_management_api_identity.Core.Constants;

namespace hotel_management_api_identity.Features.Transaction.Models
{
    public class CreatePurchaseRequest
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Enums.Category Category { get; set; }
        public string Item { get; set; }
    }

    public class CreatePurchaseResponse
    {
      public bool IsSold { get; set; }
    }
}