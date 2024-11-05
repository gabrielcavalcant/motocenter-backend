using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using System.Security.Cryptography;
using System.Text;

namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Service for handling password reset functionality, including sending reset tokens and updating passwords.
    /// </summary>
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordResetService"/> class.
        /// </summary>
        /// <param name="userRepository">Repository for accessing user data.</param>
        /// <param name="emailService">Service for sending emails.</param>
        /// <param name="unitOfWork">Unit of work for transaction management.</param>
        public PasswordResetService(IUserRepository userRepository, IEmailService emailService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Sends a password reset token to the specified user's email if they exist in the system.
        /// </summary>
        /// <param name="email">The email of the user requesting a password reset.</param>
        /// <returns>True if the reset token was sent; otherwise, false if the user was not found.</returns>
        public async Task<bool> SendPasswordResetTokenAsync(string email)
        {
            User user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            string resetToken = GenerateResetToken();
            user.PasswordResetToken = resetToken;
            user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1); // Token valid for 1 hour

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.Commit();

            await _emailService.SendPasswordResetEmailAsync(user.Email, resetToken);

            return true;
        }

        /// <summary>
        /// Resets the user's password if the provided token is valid and has not expired.
        /// </summary>
        /// <param name="email">The email of the user requesting a password reset.</param>
        /// <param name="token">The password reset token.</param>
        /// <param name="newPassword">The new password to set for the user.</param>
        /// <returns>True if the password was successfully reset; otherwise, false if the token is invalid or expired.</returns>
        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            User user = await _userRepository.GetByEmailAsync(email);
            if (user == null || user.PasswordResetToken != token || user.ResetTokenExpiry < DateTime.UtcNow)
            {
                return false;
            }

            user.Hash = HashPassword(newPassword);
            user.PasswordResetToken = null;
            user.ResetTokenExpiry = null;

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.Commit();

            return true;
        }

        /// <summary>
        /// Generates a secure random token for password reset.
        /// </summary>
        /// <returns>A base64-encoded string representing the password reset token.</returns>
        private string GenerateResetToken()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }

        /// <summary>
        /// Hashes a password using SHA-256 for secure storage.
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
    }
}
