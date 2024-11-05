using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.Auth;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Service responsible for handling authentication operations, including sign-in, sign-up, and token refresh.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="userRepository">Repository for user data access.</param>
        /// <param name="tokenService">Service for token management.</param>
        /// <param name="unitOfWork">Unit of work for transaction management.</param>
        public AuthService(IUserRepository userRepository, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Authenticates a user with provided credentials and generates access and refresh tokens.
        /// </summary>
        /// <param name="login">The sign-in request containing user credentials.</param>
        /// <returns>A <see cref="Tokens"/> object with access and refresh tokens, or null if authentication fails.</returns>
        public async Task<Tokens> SignInAsync(SignInRequest login)
        {
            User user = await _userRepository.GetByEmailAsync(login.Email);
            if (user != null && VerifyPasswordHash(login.Hash, user.Hash))
            {
                Tokens newTokens = _tokenService.GenerateTokens(user);
                user.HashedRt = ComputeHash(newTokens.RefreshToken);

                await _userRepository.UpdateAsync(user);
                await _unitOfWork.Commit();

                return newTokens;
            }
            return null;
        }

        /// <summary>
        /// Refreshes the user's authentication tokens using the provided refresh token.
        /// </summary>
        /// <param name="refresh">The refresh token request containing the refresh token.</param>
        /// <returns>A <see cref="Tokens"/> object with new access and refresh tokens, or null if the refresh fails.</returns>
        public async Task<Tokens> RefreshAsync(RefreshTokenRequest refresh)
        {
            ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(refresh.RefreshToken);
            if (principal == null)
            {
                return null;
            }

            string idString = principal.Claims.FirstOrDefault()?.Value;
            if (!Guid.TryParse(idString, out var userId))
            {
                return null;
            }

            User user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            string computedHash = ComputeHash(refresh.RefreshToken);
            if (computedHash != user.HashedRt)
            {
                return null;
            }

            Tokens newTokens = _tokenService.GenerateTokens(user);
            user.HashedRt = ComputeHash(newTokens.RefreshToken);

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.Commit();

            return newTokens;
        }

        /// <summary>
        /// Registers a new user in the system and generates tokens upon successful registration.
        /// </summary>
        /// <param name="signUp">The sign-up request containing user registration details.</param>
        /// <returns>A <see cref="Tokens"/> object with access and refresh tokens, or null if registration fails.</returns>
        public async Task<Tokens> SignUpAsync(SignUpRequest signUp)
        {
            User existingUser = await _userRepository.GetByEmailAsync(signUp.Email);
            if (existingUser != null)
            {
                return null;
            }

            string passwordHash = HashPassword(signUp.Hash);

            User newUser = new User
            {
                Email = signUp.Email,
                FullName = signUp.FullName,
                Hash = passwordHash,
            };

            await _userRepository.AddAsync(newUser);
            await _unitOfWork.Commit();

            Tokens newTokens = _tokenService.GenerateTokens(newUser);
            newUser.HashedRt = ComputeHash(newTokens.RefreshToken);

            await _userRepository.UpdateAsync(newUser);
            await _unitOfWork.Commit();

            return newTokens;
        }

        /// <summary>
        /// Computes a hash for a given token using SHA-256 encryption.
        /// </summary>
        /// <param name="token">The token to be hashed.</param>
        /// <returns>A hashed string representation of the token.</returns>
        private string ComputeHash(string token)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(token);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Hashes a password string using SHA-256 encryption for storage.
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <returns>A hashed string representation of the password.</returns>
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// Verifies that a password matches the stored hashed password.
        /// </summary>
        /// <param name="password">The plain text password provided by the user.</param>
        /// <param name="storedHash">The stored hash to compare against.</param>
        /// <returns>True if the password matches the stored hash; otherwise, false.</returns>
        private bool VerifyPasswordHash(string password, string storedHash)
        {
            string hashOfInput = HashPassword(password);
            return hashOfInput == storedHash;
        }
    }
}
