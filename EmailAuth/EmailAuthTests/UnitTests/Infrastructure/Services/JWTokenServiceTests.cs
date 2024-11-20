using EmailAuth.Infrastructure.Services;
using EmailAuth.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmailAuthTests.UnitTests.Infrastructure.Services
{
    public class JWTokenServiceTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILogger<JWTokenService>> _mockLogger;
        private readonly IJWTokenService _jwtTokenService;

        public JWTokenServiceTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<JWTokenService>>();

            // Setup mock configuration
            _mockConfiguration.Setup(c => c[It.IsAny<string>()])
                .Returns((string key) =>
                {
                    return key switch
                    {
                        "JWT:Secret" => "your-256-bit-secret",
                        "JWT:ValidIssuer" => "your-issuer",
                        "JWT:ValidAudience" => "your-audience",
                        _ => null
                    };
                });

            _jwtTokenService = new JWTokenService(_mockConfiguration.Object, _mockLogger.Object);
        }

        [Fact]
        public void GenerateToken_ShouldReturnValidToken()
        {
            // Arrange
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testuser"),
                new Claim(ClaimTypes.Email, "testuser@example.com")
            };

            // Act
            var token = _jwtTokenService.GenerateToken(authClaims);

            // Assert
            Assert.NotNull(token);
            Assert.IsType<string>(token);
        }

        [Fact]
        public void GenerateRefreshToken_ShouldReturnNonEmptyString()
        {
            // Act
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            // Assert
            Assert.NotNull(refreshToken);
            Assert.IsType<string>(refreshToken);
            Assert.NotEmpty(refreshToken);
        }

        [Fact]
        public void GetPrincipalFromExpiredToken_ShouldReturnValidClaimsPrincipal()
        {
            // Arrange
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testuser"),
                new Claim(ClaimTypes.Email, "testuser@example.com")
            };
            var token = _jwtTokenService.GenerateToken(authClaims, 1); // Generate a token that expires in 1 minute

            // Wait for the token to expire
            Thread.Sleep(TimeSpan.FromMinutes(1.1));

            // Act
            var principal = _jwtTokenService.GetPrincipalFromExpiredToken(token);

            // Assert
            Assert.NotNull(principal);
            Assert.IsType<ClaimsPrincipal>(principal);
            Assert.Equal("testuser", principal.Identity?.Name);
        }

        [Fact]
        public void GenerateToken_WithInvalidSigningKey_ShouldThrowException()
        {
            // Arrange
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testuser"),
                new Claim(ClaimTypes.Email, "testuser@example.com")
            };
            _mockConfiguration.Setup(c => c["JWT:Secret"]).Returns("short-key"); // Short key to trigger exception

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _jwtTokenService.GenerateToken(authClaims));
        }

        [Fact]
        public void GenerateToken_WithNullClaims_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _jwtTokenService.GenerateToken(null));
        }

        [Fact]
        public void GetPrincipalFromExpiredToken_WithInvalidToken_ShouldThrowException()
        {
            // Arrange
            var invalidToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0dXNlciIsImp0aSI6IjEyMzQ1NiIsImlhdCI6MTUxNjIzOTAyMn0.invalidsignature";

            // Act & Assert
            Assert.Throws<SecurityTokenException>(() => _jwtTokenService.GetPrincipalFromExpiredToken(invalidToken));
        }

        [Fact]
        public void GetPrincipalFromExpiredToken_WithDifferentAlgorithm_ShouldThrowException()
        {
            // Arrange
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testuser"),
                new Claim(ClaimTypes.Email, "testuser@example.com")
            };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_mockConfiguration.Object["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _mockConfiguration.Object["JWT:ValidIssuer"],
                audience: _mockConfiguration.Object["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512) // Different algorithm
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Wait for the token to expire
            Thread.Sleep(TimeSpan.FromMinutes(1.1));

            // Act & Assert
            Assert.Throws<SecurityTokenException>(() => _jwtTokenService.GetPrincipalFromExpiredToken(tokenString));
        }

    }
}
