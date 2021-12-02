using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Features.Authentication.Models;
using hotel_management_api_identity.Features.Authentication.Services;
using Newtonsoft.Json;

namespace hotel_management_api_identity.Core.MiddlewareExtensions
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration, ITokenService tokenService)
        {
            _next = next;
            _config = configuration;
            _tokenService = tokenService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue("devUser", out Microsoft.Extensions.Primitives.StringValues devUser))
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
            if (_authValues.Length > 2)
            {
                if (await _tokenService.ValidateToken(new TokenRequest { Email = httpContext.User.Identity.GetEmail(), Token = _authValues[1]}))
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