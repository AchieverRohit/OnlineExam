using EmailAuth.Common.Constants;
using EmailAuth.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmailAuth.Infrastructure.Services
{
    /// <summary>
    /// Service for managing JWT tokens.
    /// </summary>
    internal class JWTokenService : IJWTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<JWTokenService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="JWTokenService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        /// <param name="logger">The logger instance.</param>
        public JWTokenService(IConfiguration configuration, ILogger<JWTokenService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Generates a JWT token.
        /// </summary>
        /// <param name="authClaims">List of claims to include in the token.</param>
        /// <param name="TTLInMinutes">Token time-to-live in minutes.</param>
        /// <returns>The generated JWT token.</returns>
        public string GenerateToken(List<Claim> authClaims, int TTLInMinutes = 1)
        {
            if (authClaims == null || authClaims.Count == 0)
            {
                throw new ArgumentNullException(nameof(authClaims));
            }

            // Get the signing key from the configuration
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[Constants.JWT_Secret] ?? string.Empty));

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: _configuration[Constants.JWT_ValidIssuer],
                audience: _configuration[Constants.JWT_ValidAudience],
                expires: DateTime.UtcNow.AddMinutes(TTLInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            // Write the token to a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Generates a refresh token.
        /// </summary>
        /// <returns>The generated refresh token.</returns>
        public string GenerateRefreshToken()
        {
            // Generate a random number for the refresh token
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Gets the principal from an expired token.
        /// </summary>
        /// <param name="token">The expired JWT token.</param>
        /// <returns>The claims principal extracted from the token.</returns>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var authSigningKey = Encoding.UTF8.GetBytes(_configuration[Constants.JWT_Secret] ?? string.Empty);

            // Define token validation parameters
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(authSigningKey),
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal principal;
            try
            {
                // Validate the token
                principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                // Ensure the token is valid
                JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException(Constants.InvalidToken);
                }

                return principal;
            }
            catch (SecurityTokenException ex)
            {
                // Rethrow the exception
                throw new SecurityTokenException(Constants.InvalidToken);
            }
        }
    }
}
