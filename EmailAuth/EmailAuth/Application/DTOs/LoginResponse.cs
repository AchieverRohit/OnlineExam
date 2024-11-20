using EmailAuth.Common.Mappings;
using EmailAuth.Domain.Entities;

namespace EmailAuth.Application.DTOs
{
    /// <summary>
    /// DTO for login response.
    /// </summary>
    public class LoginResponse : IMapFrom<EmailAuthUser>
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the JWT token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
