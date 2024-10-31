using OficinaMotocenter.Application.Dto.Requests.Auth;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Tokens> SignInAsync(SignInRequest login);
        Task<Tokens> RefreshAsync(RefreshTokenRequest refresh);
        Task<Tokens> SignUpAsync(SignUpRequest signUp);
    }
}
