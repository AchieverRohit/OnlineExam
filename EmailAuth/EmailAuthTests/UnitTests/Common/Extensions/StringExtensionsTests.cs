using EmailAuth.Common.Extensions;

namespace EmailAuthTests.UnitTests.Common.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void GenerateRandomStringWithNumbers_ShouldReturnStringOfVariousLengths(int length)
        {
            // Act
            string result = StringExtensions.GenerateRandomStringWithNumbers(length);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(length, result.Length);
        }

        [Fact]
        public void GenerateRandomStringWithNumbers_ShouldReturnStringWithOnlyNumbers()
        {
            // Arrange
            int length = 10;

            // Act
            string result = StringExtensions.GenerateRandomStringWithNumbers(length);

            // Assert
            Assert.All(result, c => Assert.InRange(c, '0', '9'));
        }

        [Fact]
        public void GenerateRandomStringWithNumbers_WithDefaultLength_ShouldReturnStringOfLength6()
        {
            // Act
            string result = StringExtensions.GenerateRandomStringWithNumbers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(6, result.Length);
        }

        [Fact]
        public void GenerateRandomStringWithNumbers_ShouldReturnDifferentStringsOnSubsequentCalls()
        {
            // Arrange
            int length = 10;

            // Act
            string result1 = StringExtensions.GenerateRandomStringWithNumbers(length);
            string result2 = StringExtensions.GenerateRandomStringWithNumbers(length);

            // Assert
            Assert.NotEqual(result1, result2);
        }
    }
}
