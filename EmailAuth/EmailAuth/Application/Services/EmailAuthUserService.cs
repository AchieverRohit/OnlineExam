using AutoMapper;
using EmailAuth.Application.DTOs;
using EmailAuth.Application.Interfaces;
using EmailAuth.Application.Validators;
using EmailAuth.Common.Constants;
using EmailAuth.Common.Extensions;
using EmailAuth.Common.Models;
using EmailAuth.Domain.Entities;
using EmailAuth.Infrastructure.Persistence.Interfaces;
using EmailAuth.Infrastructure.Services.Interfaces;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;

namespace EmailAuth.Application.Services
{
    /// <summary>
    /// Service for handling email authentication-related operations.
    /// </summary>
    internal class EmailAuthUserService : IEmailAuthUserService
    {
        private readonly IEmailAuthDbContext _dbContext;
        private readonly IMailerService _mailer;
        private readonly IMapper _mapper;
        private readonly IJWTokenService _jwtService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAuthUserService"/> class.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        /// <param name="mailer">Mailer service.</param>
        /// <param name="mapper">Mapper for DTO to entity conversions.</param>
        /// <param name="jwtService">JWT service for token generation.</param>
        public EmailAuthUserService(IEmailAuthDbContext dbContext, IMailerService mailer, IMapper mapper, IJWTokenService jwtService)
        {
            _dbContext = dbContext;
            _mailer = mailer;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Adds a new user asynchronously.
        /// </summary>
        /// <param name="addUserDto">DTO containing user information.</param>
        /// <returns>A <see cref="BaseVM"/> indicating the result of the operation.</returns>
        public async Task<BaseVM> AddUserAsync(AddUserDto addUserDto)
        {
            await ValidateAsync(addUserDto, new AddUserDtoValidator());

            try
            {
                // Check if user already exists
                var existingUser = _dbContext.GetUsersQuery().FirstOrDefault(x => x.Email == addUserDto.Email.Trim());

                if (existingUser != null)
                {
                    return new BaseVM(HttpStatusCode.BadRequest, new ResponseMessage { Message = Constants.UserExist });
                }

                var newUser = _mapper.Map<EmailAuthUser>(addUserDto);
                newUser.Email = addUserDto.Email.Trim();
                newUser.GeneratedOTP = StringExtensions.GenerateRandomStringWithNumbers();

                newUser = (EmailAuthUser)_mapper.Map(newUser, newUser.GetType(), _dbContext.UserType);
                await _dbContext.AddUserAsync(newUser);

                // Send OTP via email
                await _mailer.SendByTemplate(EmailConstants.LoginEmailTitle, EmailConstants.LoginEmailBody, newUser.Email, new Dictionary<string, string>
                {
                    { EmailConstants.OTP, newUser.GeneratedOTP }
                });

                return new BaseVM(HttpStatusCode.OK, new ResponseMessage { Message = Constants.OTPSent });
            }
            catch (Exception ex)
            {
                return new BaseVM(HttpStatusCode.InternalServerError, new ResponseMessage { Message = Constants.InternalServiceError, Description = ex.Message });
            }
        }

        /// <summary>
        /// Initiates the password reset process asynchronously.
        /// </summary>
        /// <param name="dto">DTO containing the email for password reset.</param>
        /// <returns>A <see cref="BaseVM"/> indicating the result of the operation.</returns>
        public async Task<BaseVM> InitResetPasswordAsync(InitResetPasswordDto dto)
        {
            try
            {
                // Check if user already exists
                var user = _dbContext.GetUsersQuery().FirstOrDefault(x => x.Email == dto.Email.Trim());

                if (user == null)
                {
                    return new BaseVM(HttpStatusCode.BadRequest, new ResponseMessage { Message = Constants.InvalidEmail });
                }

                user.GeneratedOTP = StringExtensions.GenerateRandomStringWithNumbers();
                await _dbContext.SaveChangesAsync();

                // Send OTP via email
                await _mailer.SendByTemplate(EmailConstants.ResetPasswordEmailTitle, EmailConstants.ResetPasswordEmailBody, user.Email, new Dictionary<string, string>
                {
                    { EmailConstants.OTP, user.GeneratedOTP }
                });

                return new BaseVM(HttpStatusCode.OK, new ResponseMessage { Message = Constants.OTPSent });
            }
            catch (Exception ex)
            {
                return new BaseVM(HttpStatusCode.InternalServerError, new ResponseMessage { Message = Constants.InternalServiceError, Description = ex.Message });
            }
        }

        /// <summary>
        /// Logs in a user asynchronously.
        /// </summary>
        /// <param name="request">DTO containing login request information.</param>
        /// <returns>A <see cref="BaseVM{LoginResponse}"/> with login response details.</returns>
        public async Task<BaseVM<LoginResponse>> LoginAsync(LoginRequest request)
        {
            try
            {
                // Check if user already exists
                var user = _dbContext.GetUsersQuery().FirstOrDefault(x => x.Email == request.Email.Trim());

                if (user == null)
                {
                    return new BaseVM<LoginResponse>(HttpStatusCode.BadRequest, new ResponseMessage { Message = Constants.InvalidEmail });
                }

                if (!user.IsEmailConfirmed)
                {
                    return new BaseVM<LoginResponse>(HttpStatusCode.Unauthorized, new ResponseMessage { Message = Constants.VerifyEmail });
                }

                // Split the password hash and salt
                var passwordHashAndSalt = user.PasswordHash.Split("||");
                if (passwordHashAndSalt.Length != 2)
                {
                    return new BaseVM<LoginResponse>(HttpStatusCode.Unauthorized, new ResponseMessage { Message = Constants.SetupAccount });
                }

                // Hash the input password
                string inputPasswordHash = request.Password.GetPasswordHash(Convert.FromBase64String(passwordHashAndSalt[1]));
                bool passwordMatching = passwordHashAndSalt[0] == inputPasswordHash;
                if (!passwordMatching)
                {
                    return new BaseVM<LoginResponse>(HttpStatusCode.BadRequest, new ResponseMessage { Message = Constants.InvalidEmailOrPassword });
                }

                var result = new BaseVM<LoginResponse>()
                {
                    Data = _mapper.Map<LoginResponse>(user)
                };

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                // Generate JWT token
                result.Data.Token = _jwtService.GenerateToken(authClaims, 60);
                // Generate refresh token
                result.Data.RefreshToken = _jwtService.GenerateRefreshToken();

                user.RefreshToken = result.Data.RefreshToken;
                // Set refresh token expiry time
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(30);

                await _dbContext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                return new BaseVM<LoginResponse>(HttpStatusCode.InternalServerError, new ResponseMessage { Message = Constants.InternalServiceError, Description = ex.Message });
            }
        }

        /// <summary>
        /// Refreshes the JWT token asynchronously.
        /// </summary>
        /// <param name="refreshTokenDto">DTO containing the refresh token information.</param>
        /// <returns>A <see cref="BaseVM{RefreshTokenDto}"/> with new token details.</returns>
        public async Task<BaseVM<RefreshTokenDto>> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            await ValidateAsync(refreshTokenDto, new RefreshTokenDtoValidator());

            try
            {
                // Get principal from expired token
                var principal = _jwtService.GetPrincipalFromExpiredToken(refreshTokenDto.AccessToken);
                // Extract email claim
                var emailClaim = principal.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(emailClaim))
                {
                    throw new SecurityTokenException(Constants.InvalidToken);
                }

                var user = _dbContext.GetUsersQuery().FirstOrDefault(x => x.Email == emailClaim.Trim());

                // Return error if token is invalid
                if (user == null || user.RefreshToken != refreshTokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    return new BaseVM<RefreshTokenDto>(HttpStatusCode.BadRequest, new ResponseMessage { Message = Constants.InvalidClientReq });
                }

                // Generate JWT token
                var newAccessToken = _jwtService.GenerateToken(principal.Claims.ToList(), 60);
                // Generate refresh token
                var newRefreshToken = _jwtService.GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                // Set refresh token expiry time
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(30);

                await _dbContext.SaveChangesAsync(CancellationToken.None);

                return new BaseVM<RefreshTokenDto>
                {
                    Data = new RefreshTokenDto
                    {
                        AccessToken = newAccessToken,
                        RefreshToken = newRefreshToken
                    }
                };
            }
            catch (SecurityTokenException ex)
            {
                return new BaseVM<RefreshTokenDto>(HttpStatusCode.BadRequest, new ResponseMessage { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return new BaseVM<RefreshTokenDto>(HttpStatusCode.InternalServerError, new ResponseMessage { Message = Constants.InternalServiceError, Description = ex.Message });
            }
        }

        /// <summary>
        /// Sets the user's password asynchronously.
        /// </summary>
        /// <param name="setPasswordDto">DTO containing password and OTP details.</param>
        /// <returns>A <see cref="BaseVM"/> indicating the result of the operation.</returns>
        public async Task<BaseVM> SetPasswordAsync(SetPasswordDto setPasswordDto)
        {
            await ValidateAsync(setPasswordDto, new SetPasswordDtoValidator());

            try
            {
                var user = _dbContext.GetUsersQuery().FirstOrDefault(x => x.Email == setPasswordDto.Email.Trim());

                if (user == null)
                {
                    return new BaseVM(HttpStatusCode.BadRequest, new ResponseMessage { Message = Constants.InvalidEmail });
                }

                // Return error if email not confirmed
                if (!user.IsEmailConfirmed)
                {
                    return new BaseVM(HttpStatusCode.Unauthorized, new ResponseMessage { Message = Constants.ValidateOTP });
                }

                bool otpMatching = user.GeneratedOTP == setPasswordDto.OTP.Trim();
                if (!otpMatching || user.GeneratedOTP == string.Empty)
                {
                    return new BaseVM(HttpStatusCode.BadRequest, new ResponseMessage { Message = Constants.InvalidOTP });
                }

                setPasswordDto.Password = setPasswordDto.Password.Trim();

                // Generate password hash and salt
                var passwordHash = setPasswordDto.Password.GeneratePasswordHash(out byte[] salt);
                user.PasswordHash = $"{passwordHash}||{Convert.ToBase64String(salt)}";
                user.GeneratedOTP = string.Empty;

                await _dbContext.SaveChangesAsync();

                return new BaseVM(HttpStatusCode.OK, new ResponseMessage { Message = Constants.PasswordSent });
            }
            catch (Exception ex)
            {
                return new BaseVM(HttpStatusCode.InternalServerError, new ResponseMessage { Message = Constants.InternalServiceError, Description = ex.Message });
            }
        }

        /// <summary>
        /// Validates the OTP asynchronously.
        /// </summary>
        /// <param name="validateOTPDto">DTO containing OTP details.</param>
        /// <returns>A <see cref="BaseVM"/> indicating the result of the operation.</returns>
        public async Task<BaseVM> ValidateOTPAsync(ValidateOTPDto validateOTPDto)
        {
            try
            {
                var user = _dbContext.GetUsersQuery().FirstOrDefault(x => x.Email == validateOTPDto.Email.Trim());

                if (user == null)
                {
                    return new BaseVM(HttpStatusCode.BadRequest, new ResponseMessage { Message = Constants.InvalidEmail });
                }

                bool otpMatching = user.GeneratedOTP == validateOTPDto.OTP.Trim();

                // Check if OTP matches
                if (!otpMatching || user.GeneratedOTP == string.Empty)
                {
                    return new BaseVM(HttpStatusCode.BadRequest, new ResponseMessage { Message = Constants.InvalidOTP });
                }

                // Set email confirmed to true
                user.IsEmailConfirmed = true;
                await _dbContext.SaveChangesAsync();

                return new BaseVM(HttpStatusCode.OK, new ResponseMessage { Message = Constants.ValidOTP });
            }
            catch (Exception ex)
            {
                return new BaseVM(HttpStatusCode.InternalServerError, new ResponseMessage { Message = Constants.InternalServiceError, Description = ex.Message });
            }
        }

        /// <summary>
        /// Saves the user's profile asynchronously.
        /// </summary>
        /// <param name="saveUserProfileDto">DTO containing profile information.</param>
        /// <returns>A <see cref="BaseVM"/> indicating the result of the operation.</returns>
        public async Task<BaseVM> SaveUserProfileAsync(SaveUserProfileDto saveUserProfileDto)
        {
            try
            {
                var existingUser = _dbContext.GetUsersQuery().FirstOrDefault(x => x.Id == saveUserProfileDto.Id);

                if (existingUser == null)
                {
                    return new BaseVM(HttpStatusCode.BadRequest, new ResponseMessage { Message = Constants.UserNotFound });
                }

                _mapper.Map(saveUserProfileDto, existingUser);

                await _dbContext.SaveChangesAsync();

                return new BaseVM(HttpStatusCode.OK, new ResponseMessage { Message = Constants.UserProfileSaved });
            }
            catch (Exception ex)
            {
                return new BaseVM(HttpStatusCode.InternalServerError, new ResponseMessage { Message = Constants.InternalServiceError, Description = ex.Message });
            }
        }

        /// <summary>
        /// Validates the DTO using the specified validator.
        /// </summary>
        /// <typeparam name="T">Type of the DTO.</typeparam>
        /// <param name="dto">DTO instance to validate.</param>
        /// <param name="validator">Validator instance.</param>
        private static async Task ValidateAsync<T>(T dto, IValidator<T> validator)
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}
