using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using System.Security.Cryptography;
using System.Text;

namespace OficinaMotocenter.Application.Services
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public PasswordResetService(IUserRepository userRepository, IEmailService emailService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> SendPasswordResetTokenAsync(string email, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByEmailAsync(email, cancellationToken);
            if (user == null)
            {
                return false; // Usuário não encontrado
            }

            // Gerar token de reset de senha
            string resetToken = GenerateResetToken();
            user.PasswordResetToken = resetToken;
            user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1); // Token válido por 1 hora

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            // Enviar e-mail com o token de reset
            await _emailService.SendPasswordResetEmailAsync(user.Email, resetToken, cancellationToken);

            return true;
        }

        // Passo 2: Validar o token e resetar a senha
        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByEmailAsync(email, cancellationToken);
            if (user == null || user.PasswordResetToken != token || user.ResetTokenExpiry < DateTime.UtcNow)
            {
                return false; // Token inválido ou expirado
            }

            // Atualizar a senha do usuário
            user.Hash = HashPassword(newPassword);
            user.PasswordResetToken = null; // Limpar o token de reset
            user.ResetTokenExpiry = null;

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return true;
        }

        // Geração de token de reset de senha
        private string GenerateResetToken()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }

        // Função auxiliar para hashear senha
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
