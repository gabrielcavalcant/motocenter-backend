namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Interface for email services, providing methods for sending email notifications.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends a password reset email to the specified email address.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="resetToken">The password reset token to include in the email.</param>
        /// <returns>A task representing the asynchronous email sending operation.</returns>
        Task SendPasswordResetEmailAsync(string email, string resetToken);
    }
}
