namespace EmailAuth.Application.DTOs
{
    /// <summary>
    /// DTO for initiating password reset.
    /// </summary>
    public class InitResetPasswordDto
    {
        /// <summary>
        /// Gets or sets the email of the user requesting password reset.
        /// </summary>
        public string Email { get; set; }
    }
}
