using System.Security.Cryptography;
using System.Text;

namespace EmailAuth.Common.Extensions
{
    /// <summary>
    /// Extensions for password hashing.
    /// </summary>
    internal static class PasswordHashing
    {
        const int keySize = 64;
        const int iterations = 350000;
        static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        /// <summary>
        /// Generates a password hash with a salt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The generated salt.</param>
        /// <returns>The hashed password as a hex string.</returns>
        internal static string GeneratePasswordHash(this string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        /// <summary>
        /// Gets the password hash using the provided salt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt to use.</param>
        /// <returns>The hashed password as a hex string.</returns>
        internal static string GetPasswordHash(this string password, byte[] salt)
        {
            if (salt.Length != keySize)
            {
                throw new ArgumentException(string.Format(Constants.Constants.SaltSizeError, keySize), nameof(salt));
            }

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }
    }
}
