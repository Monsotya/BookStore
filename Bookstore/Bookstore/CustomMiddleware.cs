using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Bookstore
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("CustomMiddleware: Start");

            httpContext.Request.Headers.Add("CustomHeader", "CustomValue");

            await _next(httpContext);

            var responseStatusCode = httpContext.Response.StatusCode;

            Console.WriteLine("CustomMiddleware: End. Response code - " + responseStatusCode.ToString());
        }
    }

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
