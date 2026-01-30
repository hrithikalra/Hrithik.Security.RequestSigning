namespace Hrithik.Security.RequestSigning.Abstractions
{
    /// <summary>
    /// Provides signing secrets for a given client identifier.
    /// </summary>
    public interface ISigningKeyProvider
    {
        /// <summary>
        /// Returns the signing secret for the specified client.
        /// </summary>
        Task<string?> GetSecretAsync(string clientId, CancellationToken ct = default);
    }
}
