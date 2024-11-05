namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Interface for password reset services, providing methods for sending reset tokens and updating passwords.
    /// </summary>
    public interface IPasswordResetService
    {
        /// <summary>
        /// Sends a password reset token to the user's email.
        /// </summary>
        /// <param name="email">The email of the user requesting a password reset.</param>
        /// <returns>True if the reset token was sent successfully; otherwise, false.</returns>
        Task<bool> SendPasswordResetTokenAsync(string email);

        /// <summary>
        /// Resets the user's password using a provided reset token and new password.
        /// </summary>
        /// <param name="email">The email of the user requesting a password reset.</param>
        /// <param name="token">The password reset token provided to the user.</param>
        /// <param name="newPassword">The new password to set for the user.</param>
        /// <returns>True if the password was successfully reset; otherwise, false.</returns>
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
