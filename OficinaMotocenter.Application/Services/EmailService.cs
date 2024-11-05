using Microsoft.Extensions.Configuration;
using OficinaMotocenter.Application.Interfaces.Services;
using System.Net;
using System.Net.Mail;

namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Service for handling email operations, specifically for sending password reset emails.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;
        private readonly string _emailDisplayName;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// Configures SMTP settings and initializes email sender information.
        /// </summary>
        /// <param name="configuration">The application configuration containing SMTP and email settings.</param>
        public EmailService(IConfiguration configuration)
        {
            _fromEmail = configuration["EmailSettings:FromEmail"];
            _emailDisplayName = configuration["EmailSettings:EmailDisplayName"];
            string smtpHost = configuration["EmailSettings:SmtpHost"];
            int smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            string smtpUser = configuration["EmailSettings:SmtpUser"];
            string smtpPass = configuration["EmailSettings:SmtpPass"];

            _smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };
        }

        /// <summary>
        /// Sends a password reset email to the specified user with a link to reset their password.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="resetToken">The password reset token to include in the email.</param>
        /// <returns>A task representing the asynchronous email sending operation.</returns>
        public async Task SendPasswordResetEmailAsync(string email, string resetToken)
        {
            // Constructs the password reset URL with the provided token and email
            string resetUrl = $"https://seusistema.com/reset-password?token={resetToken}&email={email}";

            // Loads the HTML template for the password reset email
            string htmlBody;
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "PasswordResetEmail.html");
            htmlBody = await File.ReadAllTextAsync(templatePath);

            // Replaces the placeholder with the actual reset URL
            htmlBody = htmlBody.Replace("{resetUrl}", resetUrl);

            // Prepares the email message
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail, _emailDisplayName),
                Subject = "Password Reset",
                Body = htmlBody,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            // Sends the email
            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
