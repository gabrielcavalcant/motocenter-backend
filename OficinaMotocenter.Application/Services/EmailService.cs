using Microsoft.Extensions.Configuration;
using OficinaMotocenter.Application.Interfaces.Services;
using System.Net.Mail;
using System.Net;

namespace OficinaMotocenter.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;
        private readonly string _emailDisplayName;

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

        public async Task SendPasswordResetEmailAsync(string email, string resetToken, CancellationToken cancellationToken)
        {
            string resetUrl = $"https://seusistema.com/reset-password?token={resetToken}&email={email}";

            string htmlBody;
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "PasswordResetEmail.html");
            htmlBody = await File.ReadAllTextAsync(templatePath, cancellationToken);

            htmlBody = htmlBody.Replace("{resetUrl}", resetUrl);


            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail, _emailDisplayName),
                Subject = "Reset de Senha",
                Body = htmlBody,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            await _smtpClient.SendMailAsync(mailMessage, cancellationToken);
        }
    }
}
