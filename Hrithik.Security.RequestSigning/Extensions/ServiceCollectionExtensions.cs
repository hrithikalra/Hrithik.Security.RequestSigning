using Hrithik.Security.RequestSigning.Middleware;
using Hrithik.Security.RequestSigning.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hrithik.Security.RequestSigning.Extensions
{
    /// <summary>
    /// Extension methods for registering and enabling HTTP request signing.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers request signing services and configuration options.
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceCollection"/> to add services to.
        /// </param>
        /// <param name="configure">
        /// An optional delegate to configure <see cref="RequestSigningOptions"/>.
        /// </param>
        /// <returns>
        /// The same <see cref="IServiceCollection"/> instance to allow chaining.
        /// </returns>
        /// <remarks>
        /// This method registers configuration required for request signing.
        /// A concrete implementation of <see cref="Abstractions.ISigningKeyProvider"/>
        /// must be registered separately.
        /// </remarks>
        public static IServiceCollection AddRequestSigning(
            this IServiceCollection services,
            Action<RequestSigningOptions>? configure = null)
        {
            if (configure != null)
                services.Configure(configure);

            return services;
        }

        /// <summary>
        /// Adds the request signing middleware to the ASP.NET Core request pipeline.
        /// </summary>
        /// <param name="app">
        /// The <see cref="IApplicationBuilder"/> used to configure the application pipeline.
        /// </param>
        /// <returns>
        /// The same <see cref="IApplicationBuilder"/> instance to allow chaining.
        /// </returns>
        /// <remarks>
        /// This middleware should be registered early in the pipeline,
        /// before endpoints that require signed requests.
        /// </remarks>
        public static IApplicationBuilder UseRequestSigning(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestSigningMiddleware>();
        }
    }
}
