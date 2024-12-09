using OficinaMotocenter.Application.Dto.Requests.Auth;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Interface for authentication services, providing methods for sign-in, sign-up, and token refresh operations.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user and generates access and refresh tokens.
        /// </summary>
        /// <param name="login">The sign-in request containing user credentials.</param>
        /// <returns>A <see cref="Tokens"/> object with access and refresh tokens, or null if authentication fails.</returns>
        Task<Tokens> SignInAsync(SignInRequest login);

        /// <summary>
        /// Refreshes authentication tokens for a user based on the provided refresh token.
        /// </summary>
        /// <param name="refresh">The refresh token request.</param>
        /// <returns>A <see cref="Tokens"/> object with new tokens, or null if the refresh fails.</returns>
        Task<Tokens> RefreshAsync(RefreshTokenRequest refresh);

        /// <summary>
        /// Registers a new user and generates tokens for the user upon successful registration.
        /// </summary>
        /// <param name="signUp">The sign-up request containing user registration details.</param>
        /// <returns>A <see cref="Tokens"/> object with access and refresh tokens, or null if registration fails.</returns>
        Task<Tokens> SignUpAsync(SignUpRequest signUp);

        string HashPassword(string password);
    }
}
