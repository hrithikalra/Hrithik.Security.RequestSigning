using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace Hrithik.Security.RequestSigning.Canonicalization
{
    internal static class CanonicalRequestBuilder
    {
        public static async Task<string> BuildAsync(
            HttpContext context,
            string clientId,
            string nonce,
            string timestamp,
            CancellationToken ct = default)
        {
            var method = context.Request.Method.ToUpperInvariant();
            var path = context.Request.Path.ToString().ToLowerInvariant();
            var query = context.Request.QueryString.HasValue
                ? context.Request.QueryString.Value!.ToLowerInvariant()
                : string.Empty;

            string bodyHash = string.Empty;

            if (context.Request.ContentLength > 0)
            {
                context.Request.EnableBuffering();

                using var reader = new StreamReader(
                    context.Request.Body,
                    Encoding.UTF8,
                    leaveOpen: true);

                var body = await reader.ReadToEndAsync(ct);
                context.Request.Body.Position = 0;

                bodyHash = ComputeSha256(body);
            }

            return string.Join('\n',
                method,
                path,
                query,
                clientId,
                nonce,
                timestamp,
                bodyHash);
        }

        private static string ComputeSha256(string input)
        {
            using var sha = SHA256.Create();
            return Convert.ToHexString(
                sha.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }
    }
}
