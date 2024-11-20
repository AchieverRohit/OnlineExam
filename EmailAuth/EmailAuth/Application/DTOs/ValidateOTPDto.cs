namespace EmailAuth.Application.DTOs
{
    /// <summary>
    /// DTO for validating OTP.
    /// </summary>
    public class ValidateOTPDto
    {
        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the OTP for verification.
        /// </summary>
        public string OTP { get; set; }
    }
}
