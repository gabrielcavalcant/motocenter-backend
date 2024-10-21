using OficinaMotocenter.Application.Dto.Requests.Auth;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Tokens> SignInAsync(SignInRequest login, CancellationToken cancellationToken);
        Task<Tokens> RefreshAsync(RefreshTokenRequest refresh, CancellationToken cancellationToken);
        Task<Tokens> SignupAsync(SignUpRequest signup, CancellationToken cancellationToken);
    }
}
