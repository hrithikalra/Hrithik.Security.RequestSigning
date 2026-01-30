using Hrithik.Security.RequestSigning.Abstractions;

namespace Hrithik.Security.RequestSigning.Providers
{
    public sealed class InMemorySigningKeyProvider : ISigningKeyProvider
    {
        private readonly Dictionary<string, string> _keys = new();

        public InMemorySigningKeyProvider(Dictionary<string, string> keys)
        {
            _keys = keys;
        }

        public Task<string?> GetSecretAsync(string clientId, CancellationToken ct = default)
        {
            _keys.TryGetValue(clientId, out var secret);
            return Task.FromResult(secret);
        }
    }
}
