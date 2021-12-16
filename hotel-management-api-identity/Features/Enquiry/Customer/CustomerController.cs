using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Features.Enquiry.Customer.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api_identity.Features.Enquiry.Customer
{
    [Route("api/[controller]/customer")]
    [ApiController]
    public partial class EnquiryController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public EnquiryController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Route("getCustomers")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        
        public async Task<IActionResult> GetCustomers(GenericRequest genericRequest)
        {
            return Ok(await _customerService.GetCustomers(genericRequest.PageSize, genericRequest.PageNumber));
        }

        [HttpGet]
        [Route("getCustomerByEmail")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        
        public async Task<IActionResult> GetCustomerByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest();
            return Ok(await _customerService.GetCustomerByEmail(email));
        }

        [HttpGet]
        [Route("getCustomerByCode")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        
        public async Task<IActionResult> GetCustomerByCode(string customerCode)
        {
            if (string.IsNullOrEmpty(customerCode))
                return BadRequest();
            return Ok(await _customerService.GetCustomerByCode(customerCode));
        }

        [HttpGet]
        [Route("getCustomerByPhone")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        
        public async Task<IActionResult> GetCustomerByPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return BadRequest();
            return Ok(await _customerService.GetCustomerByPhone(phone));
        }
    }
}
