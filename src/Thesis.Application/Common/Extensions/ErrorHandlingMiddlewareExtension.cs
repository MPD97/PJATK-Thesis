using Microsoft.AspNetCore.Builder;
using Thesis.Application.Common.Behaviours;

namespace Thesis.Application.Common.Extensions
{
    public static class ErrorHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
