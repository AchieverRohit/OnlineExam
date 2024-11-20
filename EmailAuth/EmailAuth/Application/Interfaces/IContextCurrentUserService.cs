namespace EmailAuth.Application.Interfaces
{
    /// <summary>
    /// Interface for getting the current user context.
    /// </summary>
    public interface IContextCurrentUserService
    {
        /// <summary>
        /// Gets the user ID.
        /// </summary>
        Guid UserId { get; }
    }
}
