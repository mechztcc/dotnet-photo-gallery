using System.Net;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using AppApi.Exceptions;

namespace AppApi.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (GlobalError ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.StatusCode;

                var response = new
                {
                    status = ex.StatusCode,
                    message = ex.Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new
                {
                    status = 500,
                    message = ex.Message, // <- somente a mensagem
                                          // stackTrace = ex.StackTrace // opcional, só em dev
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));

            }
        }
    }
}