namespace EmailAuth.Application.DTOs
{
    /// <summary>
    /// DTO for login request.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}
