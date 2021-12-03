using hotel_management_api_identity.Features.Onboarding.Models;
using hotel_management_api_identity.Features.Onboarding.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api_identity.Features.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnboardingController : ControllerBase
    {
        private readonly IOnboardingService _onboardingService;
        public OnboardingController(IOnboardingService onboardingService)
        {
            _onboardingService = onboardingService;
        }
       
        [HttpPost]
        [Route("customer")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Customer(CreateCustomerRequest createCustomerRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Created("Customer", await _onboardingService.CreateCustomer(createCustomerRequest));
            
        }

        [HttpPost]
        [Route("room")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Room(CreateRoomRequest createRoomRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Created("Room", await _onboardingService.CreateRoom(createRoomRequest));
        }

        [HttpPost]
        [Route("menu")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Menu(CreateMenuRequest createMenuRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Created("Menu", await _onboardingService.CreateMenu(createMenuRequest));
        }

        [HttpPost]
        [Route("employee")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Employee(CreateEmployeeRequest createEmployeeRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Created("Employee", await _onboardingService.CreateEmployee(createEmployeeRequest));
        }
    }
}