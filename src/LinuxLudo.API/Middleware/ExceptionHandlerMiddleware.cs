using System;
using System.Threading.Tasks;
using LinuxLudo.API.Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinuxLudo.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (Exception ex)
            {
                
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