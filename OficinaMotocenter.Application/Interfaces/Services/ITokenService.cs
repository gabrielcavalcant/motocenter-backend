using OficinaMotocenter.Domain.Entities;
using System.Security.Claims;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Interface for token management services, providing methods for token generation and retrieval of claims from expired tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a pair of access and refresh tokens for the specified user.
        /// </summary>
        /// <param name="user">The user for whom tokens are generated.</param>
        /// <returns>A <see cref="Tokens"/> object containing both access and refresh tokens.</returns>
        Tokens GenerateTokens(User user);

        /// <summary>
        /// Retrieves the claims principal from an expired token without validating expiration.
        /// </summary>
        /// <param name="token">The expired JWT token.</param>
        /// <returns>The <see cref="ClaimsPrincipal"/> representing the token's claims or null if invalid.</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
