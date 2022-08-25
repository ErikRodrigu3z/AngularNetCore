using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Northwind.Api.GlobalErrorHanding
{
    public static class ExceptionMiddlewareExtencions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError => 
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        string message = $"Error: {contextFeature.Error.Message}";
                        if (contextFeature.Error.InnerException != null)
                        {
                            message = $"{contextFeature.Error.Message} ------->> {contextFeature.Error.InnerException!.Message}";                              
                        }
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = message
                        }.ToString()!);
                    }
                });
            });
        }
    }
}
