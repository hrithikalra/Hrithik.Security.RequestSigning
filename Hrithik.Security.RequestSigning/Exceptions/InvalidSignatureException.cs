namespace Hrithik.Security.RequestSigning.Exceptions
{
    public sealed class InvalidSignatureException : Exception
    {
        public string ErrorCode { get; }
        public int StatusCode { get; }

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
