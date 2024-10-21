using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OficinaMotocenter.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Tokens GenerateTokens(User user)
        {
            IConfiguration jwtSettings = _configuration.GetSection("JwtSettings");
            byte[] atSecretKey = Encoding.ASCII.GetBytes(jwtSettings["AccessTokenSecret"]);
            byte[] rtSecretKey = Encoding.ASCII.GetBytes(jwtSettings["RefreshTokenSecret"]);

            // Claims para o Access Token
            var accessTokenClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()), // Converte para string
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "User"),
                new Claim("fullName", user.FullName)
            };

            // Gerar Access Token
            var accessTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(accessTokenClaims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["AccessTokenExpiresInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(atSecretKey), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken accessToken = tokenHandler.CreateToken(accessTokenDescriptor);

            // Gerar Refresh Token
            Claim[] refreshTokenClaims =
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()), // Converte para string
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
            ];

            SecurityTokenDescriptor refreshTokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(refreshTokenClaims),
                Expires = DateTime.UtcNow.AddDays(double.Parse(jwtSettings["RefreshTokenExpiresInDays"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(rtSecretKey), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            SecurityToken refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);

            return new Tokens
            {
                AccessToken = tokenHandler.WriteToken(accessToken),
                RefreshToken = tokenHandler.WriteToken(refreshToken)
            };
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            IConfigurationSection jwtSettings = _configuration.GetSection("JwtSettings");
            byte[] secretKey = Encoding.ASCII.GetBytes(jwtSettings["RefreshTokenSecret"]);

            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateAudience = false, // Não valida o Audience
                ValidateIssuer = false,   // Não valida o Issuer
                ValidateIssuerSigningKey = true, // Valida a chave de assinatura
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateLifetime = false // Ignora a expiração do token
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Token inválido");
                }

                return principal;
            }
            catch (Exception ex)
            {
                // Log exception for troubleshooting
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null; // Retorna nulo se o token não for válido
            }
        }
    }
}
