using EmailAuth.Common.Mappings;
using EmailAuth.Domain.Entities;

namespace EmailAuth.Application.DTOs
{
    /// <summary>
    /// DTO for adding a new user.
    /// </summary>
    public class AddUserDto : IMapTo<EmailAuthUser>
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }
    }
}
