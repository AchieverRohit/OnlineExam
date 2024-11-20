namespace EmailAuth.Application.DTOs
{
    /// <summary>
    /// DTO for refresh token request.
    /// </summary>
    public class RefreshTokenDto
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
