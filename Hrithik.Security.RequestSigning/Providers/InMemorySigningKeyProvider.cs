using Hrithik.Security.RequestSigning.Abstractions;

namespace Hrithik.Security.RequestSigning.Providers
{
    /// <summary>
    /// Provides signing secrets from an in-memory key store.
    /// </summary>
    /// <remarks>
    /// This implementation is intended for development, testing,
    /// or simple scenarios where signing keys can be stored in memory.
    /// It is not recommended for production environments where
    /// secrets should be stored securely.
    /// </remarks>
    public sealed class InMemorySigningKeyProvider : ISigningKeyProvider
    {
        private readonly Dictionary<string, string> _keys = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemorySigningKeyProvider"/> class.
        /// </summary>
        /// <param name="keys">
        /// A dictionary containing client identifiers as keys
        /// and their corresponding signing secrets as values.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="keys"/> is <c>null</c>.
        /// </exception>
        public InMemorySigningKeyProvider(Dictionary<string, string> keys)
        {
            _keys = keys ?? throw new ArgumentNullException(nameof(keys));
        }

        /// <summary>
        /// Retrieves the signing secret for the specified client.
        /// </summary>
        /// <param name="clientId">The unique identifier of the client.</param>
        /// <param name="ct">
        /// A <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the signing secret if found;
        /// otherwise, <c>null</c>.
        /// </returns>
        public Task<string?> GetSecretAsync(string clientId, CancellationToken ct = default)
        {
            _keys.TryGetValue(clientId, out var secret);
            return Task.FromResult(secret);
        }
    }
}
