using System;
using System.Threading.Tasks;
using LinuxLudo.API.Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LinuxLudo.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _loggerFactory = loggerFactory;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            var logger = _loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
            try
            {
                await _next(ctx); }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error: {ex.Message}");
                var err = new ErrorResponse()
                {
                    Error = "Server encountered an unexpected error",
                    StatusCode = 500,
                    RequestId = ctx.TraceIdentifier
                };

                var res = new ObjectResult(err) {StatusCode = err.StatusCode };
                await ctx.Response.WriteAsJsonAsync(res);
            }
        }
    }
}