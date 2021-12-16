using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Features.Enquiry.Employee.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api_identity.Features.Enquiry.Employee
{
    [Route("api/[controller]/employee")]
    [ApiController]
    public partial class EnquiryController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EnquiryController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [Route("getEmployees")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        [ResponseCache(Duration = 90)]
        public async Task<IActionResult> GetEmployees(GenericRequest genericRequest)
        {
            return Ok(await _employeeService.GetEmployees(genericRequest.PageSize, genericRequest.PageNumber));
        }

        [HttpGet]
        [Route("getEmployeeByEmail")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        [ResponseCache(Duration = 90)]
        public async Task<IActionResult> GetEmployeeByEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
                return BadRequest();
            return Ok(await _employeeService.GetEmployeeByEmail(email));
        }

        [HttpGet]
        [Route("getEmployeeByPhone")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        [ResponseCache(Duration = 90)]
        public async Task<IActionResult> GetEmployeeByPhone(string phone)
        {
            if (!string.IsNullOrEmpty(phone))
                return BadRequest();
            return Ok(await _employeeService.GetEmployeeByPhone(phone));
        }
    }
}