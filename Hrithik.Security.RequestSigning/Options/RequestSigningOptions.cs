namespace Hrithik.Security.RequestSigning.Options
{
    /// <summary>
    /// Configuration options for HTTP request signing.
    /// </summary>
    public sealed class RequestSigningOptions
    {
        public string ClientIdHeader { get; set; } = "X-Client-Id";
        public string NonceHeader { get; set; } = "X-Request-Id";
        public string TimestampHeader { get; set; } = "X-Timestamp";
        public string SignatureHeader { get; set; } = "X-Signature";

        /// <summary>
        /// Maximum allowed clock skew between client and server.
        /// </summary>
        public TimeSpan AllowedClockSkew { get; set; } = TimeSpan.FromMinutes(5);
    }
}
