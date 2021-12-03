using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Features.Enquiry.Menu.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api_identity.Features.Enquiry.Menu
{
    [Route("api/[controller]/menu")]
    [ApiController]
    public partial class EnquiryController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public EnquiryController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost]
        [Route("getMenu")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetMenu(GenericRequest genericRequest)
        {
            return Ok(await _menuService.GetAllMenu(genericRequest.PageSize, genericRequest.PageNumber));
        }

        [HttpGet]
        [Route("getMenuByName")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetMenuByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                return BadRequest();
            return Ok(await _menuService.GetMenuByName(name));
        }
    }
}