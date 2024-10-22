namespace OficinaMotocenter.Application.Interfaces.Services
{
    public interface IPasswordResetService
    {
        Task<bool> SendPasswordResetTokenAsync(string email, CancellationToken cancellationToken);

        Task<bool> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken);
    }
}
