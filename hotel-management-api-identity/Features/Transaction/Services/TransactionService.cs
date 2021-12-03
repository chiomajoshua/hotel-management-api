using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers;
using hotel_management_api_identity.Core.Storage.QueryRepository;
using hotel_management_api_identity.Features.Transaction.Models;
using Newtonsoft.Json;

namespace hotel_management_api_identity.Features.Transaction.Services
{
    public interface ITransactionService : IAutoDependencyCore
    {
        /// <summary>
        /// Add Single Drink/Food Purchase
        /// </summary>
        /// <param name="createPurchaseRequest"></param>
        /// <param name="addedBy"></param>
        /// <returns></returns>
        Task<GenericResponse<CreatePurchaseResponse>> CreatePurchase(CreatePurchaseRequest createPurchaseRequest, string addedBy);

        /// <summary>
        /// Add Bulk Drink/Food Purchase
        /// </summary>
        /// <param name="createPurchaseRequest"></param>
        /// <param name="addedBy"></param>
        /// <returns></returns>
        Task<GenericResponse<CreatePurchaseResponse>> CreateBulkPurchase(List<CreatePurchaseRequest> createPurchaseRequest, string addedBy);


        /// <summary>
        /// Add Booking
        /// </summary>
        /// <param name="createBookingRequest"></param>
        /// <param name="addedBy"></param>
        /// <returns></returns>
        Task<GenericResponse<CreateBookingResponse>> CreateBooking(CreateBookingRequest createBookingRequest, string addedBy);
    }


    public class TransactionService : ITransactionService
    {
        private readonly ILogger<TransactionService> _logger;
        private readonly IDapperCommand<Core.Storage.Models.Sales> _salesCommand;
        private readonly IDapperCommand<Core.Storage.Models.Booking> _bookingCommand;
        private readonly IDapperQuery<Core.Storage.Models.Booking> _bookingQuery;
        public TransactionService(IDapperCommand<Core.Storage.Models.Sales> salesCommand, ILogger<TransactionService> logger, IDapperCommand<Core.Storage.Models.Booking> bookingCommand,
                                  IDapperQuery<Core.Storage.Models.Booking> _bookingQuery)
        {
            _salesCommand = salesCommand;
            _logger = logger;
            _bookingCommand = bookingCommand;
            _bookingQuery = _bookingQuery;
        }

        public async Task<GenericResponse<CreateBookingResponse>> CreateBooking(CreateBookingRequest createBookingRequest, string addedBy)
        {
            try
            {
                _logger.LogInformation($"Create Purchase Request ----> {JsonConvert.SerializeObject(createBookingRequest)},{addedBy}");
                await _bookingCommand.AddAsync(new Core.Storage.Models.Booking
                {
                    Room = new Core.Storage.Models.Room
                    {
                        Id = Guid.Parse(createBookingRequest.RoomId)
                    },
                    AmountPaid = createBookingRequest.AmountPaid,
                    CheckOutDate = createBookingRequest.CheckOutDate,
                    HasDiscount = createBookingRequest.HasDiscount,
                    CreatedById = addedBy,
                    ModifiedById = addedBy
                });
                return new GenericResponse<CreateBookingResponse> { IsSuccessful = true, Message = ResponseMessages.CustomerCreatedSuccessfully, Data = new CreateBookingResponse { IsSuccess = true } };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GenericResponse<CreateBookingResponse> { IsSuccessful = false, Message = ResponseMessages.GeneralError };
            }
        }

        public Task<GenericResponse<CreatePurchaseResponse>> CreateBulkPurchase(List<CreatePurchaseRequest> createPurchaseRequest, string addedBy)
        {
            //try
            //{
            //    _logger.LogInformation($"Create Purchase Request ----> {JsonConvert.SerializeObject(createPurchaseRequest)},{addedBy}");
            //    await _salesCommand.AddBatchAsync(new Core.Storage.Models.Sales
            //    {
            //        Category = createPurchaseRequest.Category,
            //        Item = createPurchaseRequest.Item,
            //        Price = createPurchaseRequest.Price,
            //        Quantity = createPurchaseRequest.Quantity,
            //        CreatedById = addedBy,
            //        ModifiedById = addedBy
            //    }, );
            //    return new GenericResponse<CreatePurchaseResponse> { IsSuccessful = true, Message = ResponseMessages.CustomerCreatedSuccessfully, Data = new CreatePurchaseResponse { IsSold = true } };
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, ex.Message);
            //    return new GenericResponse<CreatePurchaseResponse> { IsSuccessful = false, Message = ResponseMessages.GeneralError };
            //}

            throw new NotImplementedException();
        }

        public async Task<GenericResponse<CreatePurchaseResponse>> CreatePurchase(CreatePurchaseRequest createPurchaseRequest, string addedBy)
        {
            
            try
            {
                _logger.LogInformation($"Create Purchase Request ----> {JsonConvert.SerializeObject(createPurchaseRequest)},{addedBy}");               
                await _salesCommand.AddAsync(new Core.Storage.Models.Sales { Category = createPurchaseRequest.Category, Item = createPurchaseRequest.Item, Price = createPurchaseRequest.Price,
                                                                             Quantity = createPurchaseRequest.Quantity, CreatedById = addedBy, ModifiedById = addedBy});
                return new GenericResponse<CreatePurchaseResponse> { IsSuccessful = true, Message = ResponseMessages.CustomerCreatedSuccessfully, Data = new CreatePurchaseResponse { IsSold = true } };                                    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GenericResponse<CreatePurchaseResponse> { IsSuccessful = false, Message = ResponseMessages.GeneralError };
            }
        }
    }
}
