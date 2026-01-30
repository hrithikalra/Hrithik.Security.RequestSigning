using Hrithik.Security.RequestSigning.Abstractions;
using Hrithik.Security.RequestSigning.Canonicalization;
using Hrithik.Security.RequestSigning.Crypto;
using Hrithik.Security.RequestSigning.Exceptions;
using Hrithik.Security.RequestSigning.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;


namespace Hrithik.Security.RequestSigning.Middleware
{
    public sealed class RequestSigningMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestSigningOptions _options;
        private readonly ISigningKeyProvider _keyProvider;

        public RequestSigningMiddleware(
            RequestDelegate next,
            IOptions<RequestSigningOptions> options,
            ISigningKeyProvider keyProvider)
        {
            _next = next;
            _options = options.Value;
            _keyProvider = keyProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (HttpMethods.IsGet(context.Request.Method))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(_options.ClientIdHeader, out var clientId) ||
                !context.Request.Headers.TryGetValue(_options.NonceHeader, out var nonce) ||
                !context.Request.Headers.TryGetValue(_options.TimestampHeader, out var timestamp) ||
                !context.Request.Headers.TryGetValue(_options.SignatureHeader, out var signature))
            {
                throw new InvalidSignatureException(
                    "Missing request signing headers.",
                    "RS-001");
            }

            if (!long.TryParse(timestamp, out var epoch))
            {
                throw new InvalidSignatureException(
                    "Invalid timestamp.",
                    "RS-002");
            }

            var requestTime = DateTimeOffset.FromUnixTimeSeconds(epoch);
            if (Math.Abs((DateTimeOffset.UtcNow - requestTime).TotalSeconds) >
                _options.AllowedClockSkew.TotalSeconds)
            {
                throw new InvalidSignatureException(
                    "Request timestamp outside allowed clock skew.",
                    "RS-003");
            }

            var secret = await _keyProvider.GetSecretAsync(clientId!);
            if (secret == null)
            {
                throw new InvalidSignatureException(
                    "Unknown client.",
                    "RS-004",
                    StatusCodes.Status403Forbidden);
            }

            var canonical = await CanonicalRequestBuilder.BuildAsync(
                context,
                clientId!,
                nonce!,
                timestamp!);

            if (!HmacSha256Signer.Verify(canonical, secret, signature!))
            {
                throw new InvalidSignatureException(
                    "Invalid request signature.",
                    "RS-005",
                    StatusCodes.Status401Unauthorized);
            }

            await _next(context);
        }
    }
}
