using EmailAuth.Common.Mappings;
using EmailAuth.Domain.Entities;

namespace EmailAuth.Application.DTOs
{
    /// <summary>
    /// DTO for saving user profile.
    /// </summary>
    public class SaveUserProfileDto : IMapTo<EmailAuthUser>
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public Guid Id { get; set; }

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
    }
}
