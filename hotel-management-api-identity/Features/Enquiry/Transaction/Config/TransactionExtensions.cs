using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Features.Enquiry.Transaction.Model;

namespace hotel_management_api_identity.Features.Enquiry.Transaction.Config
{
    public static class TransactionExtensions
    {
        public static IEnumerable<TransactionResponse> ToTransactionsList(this List<Core.Storage.Models.Sales> salesData)
        {
            var result = new List<TransactionResponse>();
            result.AddRange(salesData.Select(data => new TransactionResponse()
            {
                Category = data.Category.Description(),
                CreatedById = data.CreatedById,
                Item = data.Item,
                ModifiedById = data.ModifiedById,
                ModifiedOn = data.ModifiedOn,
                Paid = data.Price,
                Quantity = data.Quantity,
                CreatedOn = data.CreatedOn
            }));
            return result;
        }
    }
}