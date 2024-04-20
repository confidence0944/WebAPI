using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using WebAPI.Model;

namespace WebAPI.Handler
{
    public static class GlobleExceptionHandler
    {
        public static void ConfigureExceptionHandler(this WebApplication app, NLog.Logger logger)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var path = context.Request.Path.Value;
                    if (path == null)
                    {
                        logger.Error("Unknown path error occurred!");
                        return;
                    }

                    var exceptionHandle = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandle == null)
                    {
                        logger.Error($"Unknown error occurred at path:'{path}'!");
                        return;
                    }

                    var exception = exceptionHandle.Error;

                    logger.Error(exception, $"Unhandled error occurred at path:'{path}'.");
                    ApiResponse apiResponse = new ApiResponse(ReturnCode.InternalSystemError, null);
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    await context.Response.WriteAsJsonAsync(apiResponse);
                });
            });
        }
    }
}