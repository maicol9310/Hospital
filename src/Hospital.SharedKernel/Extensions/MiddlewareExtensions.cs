using Hospital.SharedKernel.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Hospital.SharedKernel.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}