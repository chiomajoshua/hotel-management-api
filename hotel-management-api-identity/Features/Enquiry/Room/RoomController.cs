using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Features.Enquiry.Room.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api_identity.Features.Enquiry.Room
{
    [Route("api/[controller]/room")]
    [ApiController]
    public partial class EnquiryController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public EnquiryController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        [Route("getRooms")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        [ResponseCache(Duration = 90)]
        public async Task<IActionResult> GetRooms(GenericRequest genericRequest)
        {
            return Ok(await _roomService.GetRooms(genericRequest.PageSize, genericRequest.PageNumber));
        }

        [HttpGet]
        [Route("getRoomByName")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        [ResponseCache(Duration = 90)]
        public async Task<IActionResult> GetRoomByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            return Ok(await _roomService.GetRoomByName(name));
        }
    }
}
