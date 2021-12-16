using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Features.Authentication.Models;
using hotel_management_api_identity.Features.Authentication.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace hotel_management_api_identity.Core.MiddlewareExtensions
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;
        private readonly IOptions<JwtToken> _appSettings;
        public static string ValidEmail { get; set; }

        public AuthenticationMiddleware()
        {

        }
        
        public AuthenticationMiddleware(RequestDelegate next, ITokenService tokenService, IOptions<JwtToken> appSettings)
        {
            _next = next;
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

            //var currentController = httpContext.GetRouteData().Values["controller"].ToString();
            //var currentAction = httpContext.GetRouteData().Values["action"].ToString();
            //if(currentController.ToLower() == "authentication" && currentAction.ToLower() == "login")
            //{
            //    await _next(httpContext);
            //    return;
            //}
            
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
                ValidEmail = PrincipalUtilities.GetEmail(_authValues[1], _appSettings.Value.Audience, _appSettings.Value.Issuer, _appSettings.Value.Secret);
                var roles = PrincipalUtilities.GetRoles(_authValues[1], _appSettings.Value.Audience, _appSettings.Value.Issuer, _appSettings.Value.Secret);
                if (await _tokenService.ValidateToken(new TokenRequest { Email = ValidEmail, Token = _authValues[1]}))
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


        public string ActiveEmail()
        {
            return ValidEmail;
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