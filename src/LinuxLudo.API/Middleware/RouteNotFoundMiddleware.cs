using System.Text.Json;
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
            var err = new ErrorResponse("Requested endpoint dosen't exist!", 404, ctx.TraceIdentifier).Respond();
            var res = new JsonResult(err).Value;
            ctx.Response.StatusCode = 404;
            await ctx.Response.WriteAsJsonAsync(res, new JsonSerializerOptions()
            {
                IgnoreNullValues = true
            });
        }
    }
}