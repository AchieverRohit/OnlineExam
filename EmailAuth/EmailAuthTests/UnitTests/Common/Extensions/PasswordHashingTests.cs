using EmailAuth.Common.Extensions;
using System.Security.Cryptography;

namespace EmailAuthTests.UnitTests.Common.Extensions
{
    public class PasswordHashingTests
    {
        [Fact]
        public void GeneratePasswordHash_ShouldReturnHashAndSalt()
        {
            // Arrange
            string password = "password123";

            // Act
            string hash = password.GeneratePasswordHash(out byte[] salt);

            // Assert
            Assert.NotNull(hash);
            Assert.NotEmpty(hash);
            Assert.NotNull(salt);
            Assert.Equal(64, salt.Length);
        }

        [Fact]
        public void GetPasswordHash_WithValidSalt_ShouldReturnExpectedHash()
        {
            // Arrange
            string password = "password123";
            string password2 = "password123";
            password.GeneratePasswordHash(out byte[] salt);

            // Act
            string hash1 = password.GetPasswordHash(salt);
            string hash2 = password2.GetPasswordHash(salt);

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetPasswordHash_WithDifferentSalt_ShouldReturnDifferentHash()
        {
            // Arrange
            string password = "password123";
            password.GeneratePasswordHash(out byte[] salt1);
            password.GeneratePasswordHash(out byte[] salt2);

            // Act
            string hash1 = password.GetPasswordHash(salt1);
            string hash2 = password.GetPasswordHash(salt2);

            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void GeneratePasswordHash_ShouldReturnDifferentHashesForDifferentPasswords()
        {
            // Arrange
            string password1 = "password123";
            string password2 = "password456";

            // Act
            string hash1 = password1.GeneratePasswordHash(out byte[] salt1);
            string hash2 = password2.GeneratePasswordHash(out byte[] salt2);

            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void GetPasswordHash_ShouldReturnDifferentHashesForDifferentSalts()
        {
            // Arrange
            string password = "password123";
            password.GeneratePasswordHash(out byte[] salt1);
            byte[] salt2 = RandomNumberGenerator.GetBytes(64);

            // Act
            string hash1 = password.GetPasswordHash(salt1);
            string hash2 = password.GetPasswordHash(salt2);

            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void GeneratePasswordHash_ShouldThrowArgumentNullException_WhenPasswordIsNull()
        {
            // Arrange
            string password = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => password.GeneratePasswordHash(out byte[] salt));
        }

        [Fact]
        public void GetPasswordHash_ShouldThrowArgumentNullException_WhenPasswordIsNull()
        {
            // Arrange
            string password = null;
            byte[] salt = RandomNumberGenerator.GetBytes(64);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => password.GetPasswordHash(salt));
        }

        [Fact]
        public void GetPasswordHash_ShouldThrowArgumentNullException_WhenSaltIsNull()
        {
            // Arrange
            string password = "password123";
            byte[] salt = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => password.GetPasswordHash(salt));
        }

        [Fact]
        public void GetPasswordHash_ShouldThrowArgumentException_WhenSaltLengthIsInvalid()
        {
            // Arrange
            string password = "password123";
            byte[] salt = new byte[32]; // Invalid salt length

            // Act & Assert
            Assert.Throws<ArgumentException>(() => password.GetPasswordHash(salt));
        }
    }
}
