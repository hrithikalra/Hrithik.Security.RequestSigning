namespace Hrithik.Security.RequestSigning.Exceptions
{
    /// <summary>
    /// Represents an error that occurs when an HTTP request
    /// fails signature validation.
    /// </summary>
    /// <remarks>
    /// This exception is typically thrown when a request is missing
    /// required signing headers, contains an invalid timestamp,
    /// or fails cryptographic signature verification.
    /// </remarks>
    public sealed class InvalidSignatureException : Exception
    {
        /// <summary>
        /// Gets the application-specific error code associated with the failure.
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Gets the HTTP status code that should be returned to the client.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSignatureException"/> class.
        /// </summary>
        /// <param name="message">
        /// A human-readable description of the error.
        /// </param>
        /// <param name="errorCode">
        /// An application-specific error code that uniquely identifies
        /// the signature validation failure.
        /// </param>
        /// <param name="statusCode">
        /// The HTTP status code to be returned to the client.
        /// Defaults to <c>401</c> (Unauthorized).
        /// </param>
        public InvalidSignatureException(
            string message,
            string errorCode,
            int statusCode = 401)
            : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }
}
