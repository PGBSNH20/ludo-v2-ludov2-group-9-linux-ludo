using LinuxLudo.API.Middleware;
using Microsoft.AspNetCore.Builder;

namespace LinuxLudo.API.Extentions
{
    public static class ApplyCustomMiddlewareExtension
    {
        public static IApplicationBuilder ApplyCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            return app;
        }
        public static IApplicationBuilder ApplyRouteNotFoundMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RouteNotFoundMiddleware>();
            return app;
        }
    }
}