using OficinaMotocenter.Domain.Entities;
using System.Security.Claims;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    public interface ITokenService
    {
        Tokens GenerateTokens(User user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
