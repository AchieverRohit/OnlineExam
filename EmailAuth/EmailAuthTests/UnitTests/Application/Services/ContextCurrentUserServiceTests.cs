using EmailAuth.Application.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace EmailAuthTests.UnitTests.Application.Services
{
    public class ContextCurrentUserServiceTests
    {
        [Fact]
        public void UserId_ShouldReturnUserId_WhenClaimIsPresent()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            var service = new ContextCurrentUserService(httpContextAccessorMock.Object);

            // Act
            var result = service.UserId;

            // Assert
            Assert.Equal(userId, result);
        }

        [Fact]
        public void UserId_ShouldReturnEmptyGuid_WhenClaimIsNotPresent()
        {
            // Arrange
            var httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity())
            };

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            var service = new ContextCurrentUserService(httpContextAccessorMock.Object);

            // Act
            var result = service.UserId;

            // Assert
            Assert.Equal(Guid.Empty, result);
        }

        [Fact]
        public void UserId_ShouldReturnEmptyGuid_WhenHttpContextIsNull()
        {
            // Arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

            var service = new ContextCurrentUserService(httpContextAccessorMock.Object);

            // Act
            var result = service.UserId;

            // Assert
            Assert.Equal(Guid.Empty, result);
        }
    }
}
