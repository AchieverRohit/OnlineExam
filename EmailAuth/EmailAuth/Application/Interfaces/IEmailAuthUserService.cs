using EmailAuth.Application.DTOs;
using EmailAuth.Common.Models;

namespace EmailAuth.Application.Interfaces
{
    /// <summary>
    /// Interface for handling email authentication-related operations.
    /// </summary>
    public interface IEmailAuthUserService
    {
        /// <summary>
        /// Adds a new user asynchronously.
        /// </summary>
        /// <param name="addUserDto">DTO containing user information.</param>
        /// <returns>A <see cref="BaseVM"/> indicating the result of the operation.</returns>
        Task<BaseVM> AddUserAsync(AddUserDto addUserDto);

        /// <summary>
        /// Saves the user's profile asynchronously.
        /// </summary>
        /// <param name="saveUserProfileDto">DTO containing profile information.</param>
        /// <returns>A <see cref="BaseVM"/> indicating the result of the operation.</returns>
        Task<BaseVM> SaveUserProfileAsync(SaveUserProfileDto saveUserProfileDto);

        /// <summary>
        /// Logs in a user asynchronously.
        /// </summary>
        /// <param name="request">DTO containing login request information.</param>
        /// <returns>A <see cref="BaseVM{LoginResponse}"/> with login response details.</returns>
        Task<BaseVM<LoginResponse>> LoginAsync(LoginRequest request);

        /// <summary>
        /// Initiates the password reset process asynchronously.
        /// </summary>
        /// <param name="dto">DTO containing the email for password reset.</param>
        /// <returns>A <see cref="BaseVM"/> indicating the result of the operation.</returns>
        Task<BaseVM> InitResetPasswordAsync(InitResetPasswordDto initResetPasswordDto);

        /// <summary>
        /// Sets the user's password asynchronously.
        /// </summary>
        /// <param name="setPasswordDto">DTO containing password and OTP details.</param>
        /// <returns>A <see cref="BaseVM"/> indicating the result of the operation.</returns>
        Task<BaseVM> SetPasswordAsync(SetPasswordDto setPasswordDto);

        /// <summary>
        /// Validates the OTP asynchronously.
        /// </summary>
        /// <param name="validateOTPDto">DTO containing OTP details.</param>
        /// <returns>A <see cref="BaseVM"/> indicating the result of the operation.</returns>
        Task<BaseVM> ValidateOTPAsync(ValidateOTPDto validateOTPDto);

        /// <summary>
        /// Refreshes the JWT token asynchronously.
        /// </summary>
        /// <param name="refreshTokenDto">DTO containing the refresh token information.</param>
        /// <returns>A <see cref="BaseVM{RefreshTokenDto}"/> with new token details.</returns>
        Task<BaseVM<RefreshTokenDto>> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
    }
}
