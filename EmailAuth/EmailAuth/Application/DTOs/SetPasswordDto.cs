namespace EmailAuth.Application.DTOs
{
    /// <summary>
    /// DTO for setting a new password.
    /// </summary>
    public class SetPasswordDto
    {
        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the OTP for verification.
        /// </summary>
        public string OTP { get; set; }
    }
}
