using Microsoft.EntityFrameworkCore;
using EmailAuth.Infrastructure.Persistence.Interfaces;
using EmailAuth.Domain.Entities;
using Microsoft.Extensions.Configuration;
using EmailAuth.Application.Interfaces;

namespace EmailAuth.Infrastructure.Persistence
{
    /// <summary>
    /// Base class for Email Authentication Database Context.
    /// </summary>
    /// <typeparam name="TUser">User type.</typeparam>
    public abstract class EmailAuthDbContext<TUser> : DbContext, IEmailAuthDbContext where TUser : EmailAuthUser
    {
        protected readonly IContextCurrentUserService _contextCurrentUserService;
        protected readonly IConfiguration _configuration;
        protected readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAuthDbContext{TUser}"/> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
        /// <param name="contextCurrentUserService">Current user service.</param>
        /// <param name="configuration">Configuration settings.</param>
        /// <param name="connString">Database connection string.</param>
        public EmailAuthDbContext(DbContextOptions options, IContextCurrentUserService contextCurrentUserService,
            IConfiguration configuration, string connString) : base(options)
        {
            _contextCurrentUserService = contextCurrentUserService;
            _configuration = configuration;
            _connectionString = connString;
        }

        /// <summary>
        /// Configures the database context.
        /// </summary>
        /// <param name="optionsBuilder">Options builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Saves changes asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Number of state entries written to the database.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the DbSet for a specified type.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <returns>DbSet for the specified type.</returns>
        public virtual DbSet<T> GetDbSet<T>() where T : class
        {
            return Set<T>();
        }

        /// <summary>
        /// Gets the type of the user.
        /// </summary>
        public Type UserType { get { return typeof(TUser); } }

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="user">User entity.</param>
        /// <param name="save">Indicates whether to save changes.</param>
        public async Task AddUserAsync(EmailAuthUser user, bool save = true)
        {
            await GetDbSet<TUser>().AddAsync((TUser)user);
            if (save)
            {
                await SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets a queryable collection of users.
        /// </summary>
        /// <returns>Queryable collection of users.</returns>
        public IQueryable<EmailAuthUser> GetUsersQuery()
        {
            return GetDbSet<TUser>().AsQueryable();
        }
    }
}
