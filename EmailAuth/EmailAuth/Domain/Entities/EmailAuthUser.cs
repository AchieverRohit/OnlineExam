using System.ComponentModel.DataAnnotations;

namespace EmailAuth.Domain.Entities
{
    /// <summary>
    /// Represents a user entity for email authentication.
    /// This class can be inherited at the project level to add additional properties as needed.
    /// </summary>
    public class EmailAuthUser
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the generated OTP for the user.
        /// This property should be configured as a unique key at the project level.
        /// </summary>
        public string? GeneratedOTP { get; set; }

        /// <summary>
        /// Gets or sets the hashed password of the user.
        /// </summary>
        public string? PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user's email is confirmed.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the refresh token for the user.
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the expiry time for the refresh token.
        /// </summary>
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
