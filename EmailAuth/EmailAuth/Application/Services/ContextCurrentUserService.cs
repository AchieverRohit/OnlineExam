using EmailAuth.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EmailAuth.Application.Services
{
    /// <summary>
    /// Service to get the current user context from the HTTP context.
    /// </summary>
    public class ContextCurrentUserService : IContextCurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextCurrentUserService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">HTTP context accessor to access the current HTTP context.</param>
        public ContextCurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the user ID from the claims in the current HTTP context.
        /// </summary>
        public Guid UserId => GetUserId();

        /// <summary>
        /// Retrieves the user ID from the claims.
        /// </summary>
        /// <returns>The user ID as a <see cref="Guid"/>.</returns>
        private Guid GetUserId()
        {
            Claim? claim = FindClaim(ClaimTypes.NameIdentifier);
            return claim != null ? new Guid(claim.Value) : Guid.Empty;
        }

        /// <summary>
        /// Finds a specific claim by name.
        /// </summary>
        /// <param name="claimName">The name of the claim to find.</param>
        /// <returns>The found <see cref="Claim"/>, or null if not found.</returns>
        private Claim? FindClaim(string claimName)
        {
            ClaimsIdentity? claimsIdentity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst(claimName);
            return claim;
        }
    }
}
