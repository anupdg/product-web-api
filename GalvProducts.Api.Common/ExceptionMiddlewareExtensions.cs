using Microsoft.AspNetCore.Builder;

namespace GalvProducts.Api.Common
{
    /// <summary>
    /// Extend IApplicationBuilder to add custome middleware
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
