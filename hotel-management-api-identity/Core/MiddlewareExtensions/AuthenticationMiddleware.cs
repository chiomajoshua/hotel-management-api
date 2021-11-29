using Newtonsoft.Json;

namespace hotel_management_api_identity.Core.MiddlewareExtensions
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private IConfiguration _config;

        public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _config = configuration;
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

            var _authValues = _config.GetValue<string>("AccessKeys").Split(",").ToList();

            if (_authValues.Any(e => e == authHeader))
            {
                await _next(httpContext);
                return;
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
