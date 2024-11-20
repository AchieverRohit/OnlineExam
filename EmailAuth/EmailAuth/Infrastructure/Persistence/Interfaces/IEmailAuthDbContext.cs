using EmailAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailAuth.Infrastructure.Persistence.Interfaces
{
    /// <summary>
    /// Interface for Email Authentication Database Context.
    /// </summary>
    public interface IEmailAuthDbContext
    {
        /// <summary>
        /// Saves changes asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the DbSet for a specified type.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <returns>DbSet for the specified type.</returns>
        DbSet<T> GetDbSet<T>() where T : class;

        /// <summary>
        /// Gets the type of the user.
        /// </summary>
        Type UserType { get; }

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="user">User entity.</param>
        /// <param name="save">Indicates whether to save changes.</param>
        Task AddUserAsync(EmailAuthUser user, bool save = true);

        /// <summary>
        /// Gets a queryable collection of users.
        /// </summary>
        /// <returns>Queryable collection of users.</returns>
        IQueryable<EmailAuthUser> GetUsersQuery();
    }
}
