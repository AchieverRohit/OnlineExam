namespace EmailAuth.Common.Extensions
{
    /// <summary>
    /// Extensions for <see cref="string"/> type.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Generates a random string with numbers.
        /// </summary>
        /// <param name="len">The length of the random string.</param>
        /// <returns>A random string of the specified length.</returns>
        internal static string GenerateRandomStringWithNumbers(int len = 6)
        {
            return string.Join(string.Empty, Enumerable.Range(1, len).Select(x => Random.Shared.Next(0, 9)));
        }
    }
}