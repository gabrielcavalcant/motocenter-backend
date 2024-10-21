namespace OficinaMotocenter.Application.Interfaces.Services
{
    public interface IPasswordResetService
    {
        Task<bool> SendPasswordResetTokenAsync(string email);

        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
