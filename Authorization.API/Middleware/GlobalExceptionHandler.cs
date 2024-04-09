using Authorization.Infraestructure.Exceptions;
using System.Net;
using System.Text.Json;

namespace Authorization.API.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                context.Response.Headers.Add("Content-Type", "application/json");
                switch (exception)
                {
                    case RoleNotFoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                string message = context.Response.StatusCode != 500 ? exception.Message : "Unexpected error while processing the request.";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    code = context.Response.StatusCode,
                    message = message
                }));
                await context.Response.CompleteAsync();
            }
        }
    }
}
