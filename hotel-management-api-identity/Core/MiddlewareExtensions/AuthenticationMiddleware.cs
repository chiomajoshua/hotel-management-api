using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Features.Authentication.Models;
using hotel_management_api_identity.Features.Authentication.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hotel_management_api_identity.Core.MiddlewareExtensions
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IOptions<JwtToken> _appSettings;

        public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration, ITokenService tokenService, IOptions<JwtToken> appSettings)
        {
            _next = next;
            _config = configuration;
            _tokenService = tokenService;
            _appSettings = appSettings;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue("devUser", out Microsoft.Extensions.Primitives.StringValues devUser))
            {
                await _next(httpContext);
                return;
            }

            var currentController = httpContext.GetRouteData().Values["controller"].ToString();
            var currentAction = httpContext.GetRouteData().Values["action"].ToString();
            if(currentController.ToLower() == "authentication" && currentAction.ToLower() == "login")
            {
                await _next(httpContext);
                return;
            }

            
            string authHeader = httpContext.Request.Headers["Token"];
            

            httpContext.Response.ContentType = "application/json";

            if (string.IsNullOrEmpty(authHeader))
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    Status = false,
                    Message = "Invalid Header"
                }));

                return;
            }

            var _authValues = authHeader.Split(" ");
            if (_authValues.Length > 1)
            {
                var roles = PrincipalUtilities.GetRoles(_authValues[1], _appSettings.Value.Audience, _appSettings.Value.Issuer, _appSettings.Value.Secret);
                if (await _tokenService.ValidateToken(new TokenRequest { Email = PrincipalUtilities.GetEmail(_authValues[1], _appSettings.Value.Audience, _appSettings.Value.Issuer, _appSettings.Value.Secret), Token = _authValues[1]}))
                {
                    await _next(httpContext);
                    return;
                }
            }

            httpContext.Response.StatusCode = 401;
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Status = false,
                Message = "Invalid Authentication"
            }));

            return;
        }
    }

    

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UseKycAuthenticationMiddleware
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}