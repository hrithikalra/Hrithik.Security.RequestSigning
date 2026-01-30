using Hrithik.Security.RequestSigning.Middleware;
using Hrithik.Security.RequestSigning.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hrithik.Security.RequestSigning.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRequestSigning(
            this IServiceCollection services,
            Action<RequestSigningOptions>? configure = null)
        {
            if (configure != null)
                services.Configure(configure);

            return services;
        }

        public static IApplicationBuilder UseRequestSigning(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestSigningMiddleware>();
        }
    }
}
