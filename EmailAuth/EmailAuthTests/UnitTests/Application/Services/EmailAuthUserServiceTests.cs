using AutoMapper;
using EmailAuth.Application.DTOs;
using EmailAuth.Application.Services;
using EmailAuth.Common.Constants;
using EmailAuth.Domain.Entities;
using EmailAuth.Infrastructure.Persistence.Interfaces;
using EmailAuth.Infrastructure.Services.Interfaces;
using Moq;
using System.Net;
using System.Security.Claims;

namespace EmailAuthTests.UnitTests.Application.Services
{
    public class EmailAuthUserServiceTests
    {
        private readonly Mock<IEmailAuthDbContext> _dbContextMock;
        private readonly Mock<IMailerService> _mailerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IJWTokenService> _jwtServiceMock;
        private readonly EmailAuthUserService _emailAuthUserService;

        public EmailAuthUserServiceTests()
        {
            _dbContextMock = new Mock<IEmailAuthDbContext>();
            _mailerMock = new Mock<IMailerService>();
            _mapperMock = new Mock<IMapper>();
            _jwtServiceMock = new Mock<IJWTokenService>();

            _emailAuthUserService = new EmailAuthUserService(
                _dbContextMock.Object,
                _mailerMock.Object,
                _mapperMock.Object,
                _jwtServiceMock.Object);
        }

        [Fact]
        public async Task AddUserAsync_ShouldReturnBadRequest_WhenUserAlreadyExists()
        {
            // Arrange
            var addUserDto = new AddUserDto { Email = "test@example.com" };
            var existingUser = new EmailAuthUser { Email = addUserDto.Email };

            _dbContextMock.Setup(db => db.GetUsersQuery()).Returns(new List<EmailAuthUser> { existingUser }.AsQueryable());

            // Act
            var result = await _emailAuthUserService.AddUserAsync(addUserDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.Status);
            Assert.Equal(Constants.UserExist, result.Messages[0].Message);
        }

        [Fact]
        public async Task AddUserAsync_ShouldReturnOK_WhenUserIsSuccessfullyAdded()
        {
            // Arrange
            var addUserDto = new AddUserDto { Email = "test@example.com" };
            var newUser = new EmailAuthUser { Email = "test@example.com" };

            _dbContextMock.Setup(db => db.GetUsersQuery()).Returns(new List<EmailAuthUser>().AsQueryable());
            _dbContextMock.Setup(db => db.UserType).Returns(typeof(EmailAuthUser));
            _mapperMock.Setup(m => m.Map<EmailAuthUser>(addUserDto)).Returns(newUser);
            _mapperMock.Setup(m => m.Map(It.IsAny<EmailAuthUser>(), It.IsAny<Type>(), It.IsAny<Type>()))
                .Returns((object source, Type sourceType, Type destinationType) => source);
            _dbContextMock.Setup(db => db.AddUserAsync(It.IsAny<EmailAuthUser>(), It.IsAny<bool>())).Returns(Task.CompletedTask);
            _mailerMock.Setup(mailer => mailer.SendByTemplate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
               .Returns(Task.CompletedTask);

            // Act
            var result = await _emailAuthUserService.AddUserAsync(addUserDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.Status);
            Assert.Equal(Constants.OTPSent, result.Messages[0].Message);
        }

        [Fact]
        public async Task InitResetPasswordAsync_ShouldReturnBadRequest_WhenUserDoesNotExist()
        {
            // Arrange
            var dto = new InitResetPasswordDto { Email = "test@example.com" };

            _dbContextMock.Setup(db => db.GetUsersQuery()).Returns(new List<EmailAuthUser>().AsQueryable());
            _dbContextMock.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _mailerMock.Setup(mailer => mailer.SendByTemplate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
               .Returns(Task.CompletedTask);

            // Act
            var result = await _emailAuthUserService.InitResetPasswordAsync(dto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.Status);
            Assert.Equal(Constants.InvalidEmail, result.Messages[0].Message);
        }

        [Fact]
        public async Task InitResetPasswordAsync_ShouldReturnOK_WhenOTPSuccessfullySent()
        {
            // Arrange
            var dto = new InitResetPasswordDto { Email = "test@example.com" };
            var user = new EmailAuthUser { Email = dto.Email };

            _dbContextMock.Setup(db => db.GetUsersQuery()).Returns(new List<EmailAuthUser> { user }.AsQueryable());
            _dbContextMock.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _mailerMock.Setup(m => m.SendByTemplate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.CompletedTask);

            // Act
            var result = await _emailAuthUserService.InitResetPasswordAsync(dto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.Status);
            Assert.Equal(Constants.OTPSent, result.Messages[0].Message);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnBadRequest_WhenUserDoesNotExist()
        {
            // Arrange
            var request = new LoginRequest { Email = "test@example.com", Password = "password" };

            _dbContextMock.Setup(db => db.GetUsersQuery()).Returns(new List<EmailAuthUser>().AsQueryable());

            // Act
            var result = await _emailAuthUserService.LoginAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.Status);
            Assert.Equal(Constants.InvalidEmail, result.Messages[0].Message);
        }

        [Fact]
        public async Task RefreshTokenAsync_ShouldReturnBadRequest_WhenRefreshTokenIsInvalid()
        {
            // Arrange
            var refreshTokenDto = new RefreshTokenDto { AccessToken = "accessToken", RefreshToken = "refreshToken" };
            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Email, "test@example.com") }));

            _jwtServiceMock.Setup(j => j.GetPrincipalFromExpiredToken(refreshTokenDto.AccessToken)).Returns(principal);
            _dbContextMock.Setup(db => db.GetUsersQuery()).Returns(new List<EmailAuthUser>().AsQueryable());

            // Act
            var result = await _emailAuthUserService.RefreshTokenAsync(refreshTokenDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.Status);
            Assert.Equal(Constants.InvalidClientReq, result.Messages[0].Message);
        }

        [Fact]
        public async Task RefreshTokenAsync_ShouldReturnOK_WhenRefreshTokenIsSuccessfullyRefreshed()
        {
            // Arrange
            var refreshTokenDto = new RefreshTokenDto { AccessToken = "accessToken", RefreshToken = "refreshToken" };
            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Email, "test@example.com") }));
            var user = new EmailAuthUser
            {
                Email = "test@example.com",
                RefreshToken = refreshTokenDto.RefreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddMinutes(1)
            };

            _jwtServiceMock.Setup(j => j.GetPrincipalFromExpiredToken(refreshTokenDto.AccessToken)).Returns(principal);
            _dbContextMock.Setup(db => db.GetUsersQuery()).Returns(new List<EmailAuthUser> { user }.AsQueryable());
            _jwtServiceMock.Setup(j => j.GenerateToken(It.IsAny<List<Claim>>(), 60)).Returns("newToken");
            _jwtServiceMock.Setup(j => j.GenerateRefreshToken()).Returns("newRefreshToken");
            _dbContextMock.Setup(db => db.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            // Act
            var result = await _emailAuthUserService.RefreshTokenAsync(refreshTokenDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.Status);
            Assert.NotNull(result.Data.AccessToken);
            Assert.NotNull(result.Data.RefreshToken);
        }

        [Fact]
        public async Task SetPasswordAsync_ShouldReturnBadRequest_WhenUserDoesNotExist()
        {
            // Arrange
            var setPasswordDto = new SetPasswordDto { Email = "test@example.com", OTP = "123456", Password = "new1#Password" };

            _dbContextMock.Setup(db => db.GetUsersQuery()).Returns(new List<EmailAuthUser>().AsQueryable());

            // Act
            var result = await _emailAuthUserService.SetPasswordAsync(setPasswordDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.Status);
            Assert.Equal(Constants.InvalidEmail, result.Messages[0].Message);
        }

        [Fact]
        public async Task SetPasswordAsync_ShouldReturnOK_WhenPasswordIsSuccessfullySet()
        {
            // Arrange
            var setPasswordDto = new SetPasswordDto { Email = "test@example.com", OTP = "123456", Password = "new1#Password" };
            var user = new EmailAuthUser { Email = setPasswordDto.Email, GeneratedOTP = setPasswordDto.OTP, IsEmailConfirmed = true };

            _dbContextMock.Setup(db => db.GetUsersQuery()).Returns(new List<EmailAuthUser> { user }.AsQueryable());
            _dbContextMock.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _emailAuthUserService.SetPasswordAsync(setPasswordDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.Status);
            Assert.Equal(Constants.PasswordSent, result.Messages[0].Message);
        }

        [Fact]
        public async Task ValidateOTPAsync_ShouldReturnBadRequest_WhenUserDoesNotExist()
        {
            // Arrange
            var validateOTPDto = new ValidateOTPDto { Email = "test@example.com", OTP = "123456" };

            _dbContextMock.Setup(db => db.GetUsersQuery()).Returns(new List<EmailAuthUser>().AsQueryable());

            // Act
            var result = await _emailAuthUserService.ValidateOTPAsync(validateOTPDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.Status);
            Assert.Equal(Constants.InvalidEmail, result.Messages[0].Message);
        }

        [Fact]
        public async Task ValidateOTPAsync_ShouldReturnOK_WhenOTPIsValid()
        {
            // Arrange
            var validateOTPDto = new ValidateOTPDto { Email = "test@example.com", OTP = "123456" };
            var user = new EmailAuthUser { Email = validateOTPDto.Email, GeneratedOTP = validateOTPDto.OTP };

            _dbContextMock.Setup(db => db.GetUsersQuery()).Returns(new List<EmailAuthUser> { user }.AsQueryable());
            _dbContextMock.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _emailAuthUserService.ValidateOTPAsync(validateOTPDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.Status);
            Assert.Equal(Constants.ValidOTP, result.Messages[0].Message);
        }

        [Fact]
        public async Task SaveUserProfileAsync_ShouldReturnBadRequest_WhenUserDoesNotExist()
        {
            // Arrange
            var saveUserProfileDto = new SaveUserProfileDto { Id = Guid.NewGuid() };

            _dbContextMock.Setup(db => db.GetUsersQuery()).Returns(new List<EmailAuthUser>().AsQueryable());

            // Act
            var result = await _emailAuthUserService.SaveUserProfileAsync(saveUserProfileDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.Status);
            Assert.Equal(Constants.UserNotFound, result.Messages[0].Message);
        }

        [Fact]
        public async Task SaveUserProfileAsync_ShouldReturnOK_WhenUserProfileIsSuccessfullySaved()
        {
            // Arrange
            var saveUserProfileDto = new SaveUserProfileDto { Id = Guid.NewGuid() };
            var user = new EmailAuthUser { Id = saveUserProfileDto.Id };

            _dbContextMock.Setup(db => db.GetUsersQuery()).Returns(new List<EmailAuthUser> { user }.AsQueryable());
            _dbContextMock.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _emailAuthUserService.SaveUserProfileAsync(saveUserProfileDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.Status);
            Assert.Equal(Constants.UserProfileSaved, result.Messages[0].Message);
        }
    }
}
