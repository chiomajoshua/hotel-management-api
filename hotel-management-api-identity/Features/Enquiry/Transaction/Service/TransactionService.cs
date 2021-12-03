using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers;
using hotel_management_api_identity.Core.Storage.QueryRepository;
using hotel_management_api_identity.Features.Enquiry.Transaction.Config;
using hotel_management_api_identity.Features.Enquiry.Transaction.Model;

namespace hotel_management_api_identity.Features.Enquiry.Transaction.Service
{
    public interface ITransactionService : IAutoDependencyCore
    {
        /// <summary>
        /// Gets All Transactions
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Task<GenericResponse<IEnumerable<TransactionResponse>>> GetTransactions(int pageSize, int pageNumber);

        /// <summary>
        /// Get All Transactions By Employee
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Task<GenericResponse<IEnumerable<TransactionResponse>>> GetTransactionsByEmployee(string email, int pageSize, int pageNumber);

        
        /// <summary>
        /// Get Transactions By Date Range
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<GenericResponse<IEnumerable<TransactionResponse>>> GetTransactionsByDateRange(DateTimeOffset startDate, DateTimeOffset endDate);
    }
    public class TransactionService : ITransactionService
    {
        private readonly ILogger<TransactionService> _logger;
        private readonly IDapperQuery<Core.Storage.Models.Sales> _salesQuery;

        public TransactionService(IDapperQuery<Core.Storage.Models.Sales> salesQuery,
                               ILogger<TransactionService> logger)
        {
            _salesQuery = salesQuery;
            _logger = logger;
        }    

        public async Task<GenericResponse<IEnumerable<TransactionResponse>>> GetTransactions(int pageSize, int pageNumber)
        {
            try
            {
                var result = await _salesQuery.GetByAsync(pageSize, pageNumber);
                if (result.Any()) return new GenericResponse<IEnumerable<TransactionResponse>> { Data = result.ToList().ToTransactionsList(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<IEnumerable<TransactionResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError("GetTransactions Error", ex.Message);
                return new GenericResponse<IEnumerable<TransactionResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }

        public async Task<GenericResponse<IEnumerable<TransactionResponse>>> GetTransactionsByEmployee(string email, int pageSize, int pageNumber)
        {
            try
            {
                var query = new Dictionary<string, string>() { { "createdbyId", email } };
                var result = await _salesQuery.GetByAsync(query, pageSize, pageNumber);
                if (result.Any()) return new GenericResponse<IEnumerable<TransactionResponse>> { Data = result.ToList().ToTransactionsList(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<IEnumerable<TransactionResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError("GetTransactionsByEmployee Error", ex.Message);
                return new GenericResponse<IEnumerable<TransactionResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }

        public async Task<GenericResponse<IEnumerable<TransactionResponse>>> GetTransactionsByDateRange(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            try
            {
                var query = new Dictionary<string, DateTimeOffset>() { { "startDate", startDate }, { "endDate", endDate } };
                var result = await _salesQuery.GetByDateRangeAsync(query);
                if (result.Any()) return new GenericResponse<IEnumerable<TransactionResponse>> { Data = result.ToList().ToTransactionsList(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<IEnumerable<TransactionResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError("GetTransactionsByDateRange Error", ex.Message);
                return new GenericResponse<IEnumerable<TransactionResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }
    }
}