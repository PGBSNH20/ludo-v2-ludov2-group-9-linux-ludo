using System.Threading.Tasks;
using LinuxLudo.API.Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinuxLudo.API.Middleware
{
    public class RouteNotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public RouteNotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            var err = new ErrorResponse()
            {
                Error = "Requested endpoint dosen't exist!",
                StatusCode = 404,
                RequestId = ctx.TraceIdentifier
            };

            var res = new ObjectResult(err) {StatusCode = err.StatusCode };
            await ctx.Response.WriteAsJsonAsync(res);
        }
    }
}