using System.Security.Claims;

namespace EmailAuth.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// Interface for JWT token service.
    /// </summary>
    internal interface IJWTokenService
    {
        /// <summary>
        /// Generates a JWT token.
        /// </summary>
        /// <param name="authClaims">List of claims to include in the token.</param>
        /// <param name="TTLInMinutes">Token time-to-live in minutes.</param>
        /// <returns>The generated JWT token.</returns>
        string GenerateToken(List<Claim> authClaims, int TTLInMinutes = 1);

        /// <summary>
        /// Generates a refresh token.
        /// </summary>
        /// <returns>The generated refresh token.</returns>
        string GenerateRefreshToken();

        /// <summary>
        /// Gets the principal from an expired token.
        /// </summary>
        /// <param name="token">The expired JWT token.</param>
        /// <returns>The claims principal extracted from the token.</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}