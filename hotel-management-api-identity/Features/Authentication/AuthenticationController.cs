using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Features.Authentication.Models;
using hotel_management_api_identity.Features.Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api_identity.Features.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;
        public AuthenticationController(ITokenService tokenService, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _tokenService = tokenService;
        }




        [HttpPost]
        [Route("login")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _authenticationService.ValidateCredentials(loginRequest))
            {
                var token = _tokenService.CreateToken(loginRequest.Email);
                if (!string.IsNullOrEmpty(token))
                return Ok(new GenericResponse<LoginResponse> { IsSuccessful = true, Data = new LoginResponse { Email = loginRequest.Email, Token = token}, Message = "Login Successful" });

                return Unauthorized(new GenericResponse<LoginResponse> { IsSuccessful = false, Message = "Login Unsuccessful" });
            }

            return Unauthorized(new GenericResponse<LoginResponse> { IsSuccessful = false, Message = "Login Unsuccessful" });
        }
    }
}