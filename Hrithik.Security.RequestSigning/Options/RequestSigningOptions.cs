namespace Hrithik.Security.RequestSigning.Options
{
    /// <summary>
    /// Represents configuration options used for HTTP request signing and validation.
    /// </summary>
    /// <remarks>
    /// These options define the HTTP headers and security constraints
    /// required to generate and validate request signatures.
    /// </remarks>
    public sealed class RequestSigningOptions
    {
        /// <summary>
        /// Gets or sets the HTTP header name that contains the client identifier.
        /// </summary>
        /// <remarks>
        /// Default value: <c>X-Client-Id</c>
        /// </remarks>
        public string ClientIdHeader { get; set; } = "X-Client-Id";

        /// <summary>
        /// Gets or sets the HTTP header name used to carry a unique request nonce.
        /// </summary>
        /// <remarks>
        /// This value is typically used to prevent replay attacks.
        /// Default value: <c>X-Request-Id</c>
        /// </remarks>
        public string NonceHeader { get; set; } = "X-Request-Id";

        /// <summary>
        /// Gets or sets the HTTP header name that contains the request timestamp.
        /// </summary>
        /// <remarks>
        /// The timestamp is validated against the server time
        /// to detect expired or replayed requests.
        /// Default value: <c>X-Timestamp</c>
        /// </remarks>
        public string TimestampHeader { get; set; } = "X-Timestamp";

        /// <summary>
        /// Gets or sets the HTTP header name that contains the request signature.
        /// </summary>
        /// <remarks>
        /// The signature is computed using the request payload,
        /// headers, and a shared secret.
        /// Default value: <c>X-Signature</c>
        /// </remarks>
        public string SignatureHeader { get; set; } = "X-Signature";

        /// <summary>
        /// Gets or sets the maximum allowed clock skew between the client and the server.
        /// </summary>
        /// <remarks>
        /// Requests with timestamps outside this window will be rejected.
        /// Default value: 5 minutes.
        /// </remarks>
        public TimeSpan AllowedClockSkew { get; set; } = TimeSpan.FromMinutes(5);
    }
}
