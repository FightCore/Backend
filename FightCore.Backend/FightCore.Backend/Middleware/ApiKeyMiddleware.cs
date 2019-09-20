using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FightCore.Services;
using Jil;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace FightCore.Backend.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IApiClientService apiClientService)
        {
            // If we are calling from localhost, don't bother using an API key.
            if (context.Connection.RemoteIpAddress.Equals(IPAddress.Parse("::1")))
            {
                await _next(context);
                return;
            }

            // API Key was not found.
            if (!context.Request.Headers.ContainsKey("x-api-key"))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(JSON.Serialize(new
                {
                    message = "No API key supplied.",
                    errorCode = "noApiKey"
                }));
                return;
            }

            var apiClient = await apiClientService.GetForKeyAsync(context.Request.Headers["x-api-key"]);

            if (apiClient == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(JSON.Serialize(new
                {
                    message = "An invalid API Key was supplied.",
                    errorCode = "wrongApiKey"
                }));
                return;
            }

            // Continue executing.
            await _next(context);
        }
    }

    public static class ApiKeyMiddlewareExtension
    {
        public static IApplicationBuilder UseApiKey(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyMiddleware>();
        }
    }
}
