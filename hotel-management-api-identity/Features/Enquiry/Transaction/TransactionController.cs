using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Features.Enquiry.Transaction.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api_identity.Features.Enquiry.Transaction
{
    [Route("api/[controller]/transactions")]
    [ApiController]
    public partial class EnquiryController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public EnquiryController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }


        [HttpPost]
        [Route("getAllTransactions")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetAllTransactions(GenericRequest genericRequest)
        {

            return Ok(await _transactionService.GetTransactions(genericRequest.PageSize, genericRequest.PageNumber));
        }

        [HttpPost]
        [Route("getTransactionsByDateRange")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetTransactionsByDateRange(GetByDateRangeRequest getByDateRangeRequest)
        {
            return Ok(await _transactionService.GetTransactionsByDateRange(getByDateRangeRequest.StartDate, getByDateRangeRequest.EndDate));
        }

        [HttpPost]
        [Route("getTransactionsByEmployee")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetTransactionsByEmployee(GenericRequest genericRequest, string email)
        {
            return Ok(await _transactionService.GetTransactionsByEmployee(email: email, genericRequest.PageSize, genericRequest.PageNumber));
        }
    }
}
