using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Middlewares
{
    public class MyMiddleware
    {
        public readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public MyMiddleware(RequestDelegate next,ILoggerFactory logFactory)
        {
            _next = next;
            _logger = logFactory.CreateLogger("MyMiddleware");
        }
        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("MyMiddleware executing here");
            await _next(httpContext);
        }



    }
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}
