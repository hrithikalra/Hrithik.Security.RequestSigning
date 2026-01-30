using System.Security.Cryptography;
using System.Text;

namespace Hrithik.Security.RequestSigning.Crypto
{
    internal static class HmacSha256Signer
    {
        public static string Sign(string canonical, string secret)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            return Convert.ToBase64String(
                hmac.ComputeHash(Encoding.UTF8.GetBytes(canonical)));
        }

        public static bool Verify(string canonical, string secret, string signature)
        {
            var expected = Sign(canonical, secret);
            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(expected),
                Encoding.UTF8.GetBytes(signature));
        }
    }
}
