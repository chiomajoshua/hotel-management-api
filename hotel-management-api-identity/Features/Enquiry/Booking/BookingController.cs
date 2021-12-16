using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Features.Enquiry.Booking.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api_identity.Features.Enquiry.Booking
{
    [Route("api/[controller]/booking")]
    [ApiController]
    public partial class EnquiryController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public EnquiryController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Route("getBookings")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        [ResponseCache(Duration = 90)]
        public async Task<IActionResult> GetBookings(GenericRequest genericRequest)
        {
            return Ok(await _bookingService.GetAllBookings(genericRequest.PageSize, genericRequest.PageNumber));
        }
    }
}