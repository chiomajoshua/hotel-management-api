using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Core.Storage.Models;
using hotel_management_api_identity.Features.Enquiry.Transaction.Model;
using hotel_management_api_identity.Features.Transaction.Models;

namespace hotel_management_api_identity.Features.Enquiry.Transaction.Config
{
    public static class TransactionExtensions
    {
        public static IEnumerable<TransactionResponse> ToTransactionsList(this List<Core.Storage.Models.Sales> salesData)
        {
            var transactionDetail = new List<TransactionDetailResponse>();
            var result = new List<TransactionResponse>();
            result.AddRange(salesData.Select(data => new TransactionResponse()
            {               
                CreatedById = data.CreatedById,
                ModifiedById = data.ModifiedById,
                ModifiedOn = data.ModifiedOn,
                CreatedOn = data.CreatedOn,
                OrderCode = data.OrderCode,
                Total = data.Total
            }));
            return result;
        }

        public static IEnumerable<TransactionDetailResponse> ToSalesDetailsList(this List<SaleDetails> saleDetailsData)
        {
            var result = new List<TransactionDetailResponse>();
            result.AddRange(saleDetailsData.Select(data => new TransactionDetailResponse()
            {
                Category = data.Category.Description(),
                Item = data.Item,
                Price = data.Price,
                Quantity = data.Quantity
            }));
            return result;
        }

        public static IEnumerable<SaleDetails> ToDbSaleDetails(this List<CreatePurchaseDetailsRequest> createPurchaseDetailsRequests)
        {
            var result = new List<SaleDetails>();
            result.AddRange(createPurchaseDetailsRequests.Select(data => new SaleDetails()
            {
                Category = data.Category,
                Item = data.Item,
                Price = data.Price,
                Quantity = data.Quantity
            }));
            return result;
        }
    }
}