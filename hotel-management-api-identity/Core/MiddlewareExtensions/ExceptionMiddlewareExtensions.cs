using hotel_management_api_identity.Core.Constants;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace hotel_management_api_identity.Core.MiddlewareExtensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    if (contextFeature != null)
                    {
                        Log.Warning($"ExceptionFailure: {JsonConvert.SerializeObject(contextFeature.Error)}.");
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new BaseResponse
                        {
                            IsSuccessful = false,
                            Message = "Sorry, something went wrong",
                            StatusCode = Core.Constants.StatusCodes.GeneralError
                        }));
                    }
                });
            });
        }
    }
}
