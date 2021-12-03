using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Core.MiddlewareExtensions;
using hotel_management_api_identity.Features.Transaction.Models;
using hotel_management_api_identity.Features.Transaction.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api_identity.Features.Sales
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("sale")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Sale(CreatePurchaseRequest createPurchaseRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Created("Sales", await _transactionService.CreatePurchase(createPurchaseRequest, new AuthenticationMiddleware().ActiveEmail()));
        }

        [HttpPost]
        [Route("booking")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Booking(CreateBookingRequest createBookingRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Created("Booking", await _transactionService.CreateBooking(createBookingRequest, new AuthenticationMiddleware().ActiveEmail()));
        }
    }
}