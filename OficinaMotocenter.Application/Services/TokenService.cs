using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Service responsible for generating and validating JWT tokens for user authentication.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="configuration">Configuration settings for JWT, including secrets and expiration times.</param>
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates access and refresh tokens for a specified user.
        /// </summary>
        /// <param name="user">The user for whom tokens are generated.</param>
        /// <returns>A <see cref="Tokens"/> object containing both access and refresh tokens.</returns>
        public Tokens GenerateTokens(User user)
        {
            IConfiguration jwtSettings = _configuration.GetSection("JwtSettings");
            byte[] atSecretKey = Encoding.ASCII.GetBytes(jwtSettings["AccessTokenSecret"]);
            byte[] rtSecretKey = Encoding.ASCII.GetBytes(jwtSettings["RefreshTokenSecret"]);

            // Claims for Access Token
            var accessTokenClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()), // Converts to string
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "User"),
                new Claim("fullName", user.FullName)
            };

            // Generate Access Token
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

        /// <summary>
        /// Retrieves the principal (user information) from an expired token without validating its expiration.
        /// Useful for obtaining user claims during token renewal.
        /// </summary>
        /// <param name="token">The expired JWT token.</param>
        /// <returns>The <see cref="ClaimsPrincipal"/> representing the token's claims or null if invalid.</returns>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            IConfigurationSection jwtSettings = _configuration.GetSection("JwtSettings");
            byte[] secretKey = Encoding.ASCII.GetBytes(jwtSettings["RefreshTokenSecret"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, // Do not validate Audience
                ValidateIssuer = false,   // Do not validate Issuer
                ValidateIssuerSigningKey = true, // Validate signing key
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateLifetime = false // Ignore token expiration
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                return principal;
            }
            catch (Exception ex)
            {
                // Log exception for troubleshooting
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null; // Returns null if the token is invalid
            }
        }
    }
}
