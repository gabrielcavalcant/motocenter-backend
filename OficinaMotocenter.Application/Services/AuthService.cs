using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.Auth;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OficinaMotocenter.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Tokens> SignInAsync(SignInRequest login)
        {
            // Verificar se o usuário existe
            User user = await _userRepository.GetByEmailAsync(login.Email);
            if (user != null && VerifyPasswordHash(login.Hash, user.Hash))
            {
                Tokens newTokens = _tokenService.GenerateTokens(user);
                // Atualizar o campo HashedRt com o novo Refresh Token (hash)
                user.HashedRt = ComputeHash(newTokens.RefreshToken);

                await _userRepository.UpdateAsync(user); // Atualiza o usuário
                await _unitOfWork.Commit(); // Salva as mudanças

                return newTokens;
            }
            return null; // Retorna null se o login falhar
        }

        public async Task<Tokens> RefreshAsync(RefreshTokenRequest refresh)
        {
            // Validar o token existente (refresh token)
            ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(refresh.RefreshToken);
            if (principal == null)
            {
                return null; // Token inválido
            }

            // Obter o ID do usuário do primeiro claim
            string idString = principal.Claims.FirstOrDefault()?.Value; // Obter o primeiro claim

            // Converter o ID de string para GUID
            if (!Guid.TryParse(idString, out var userId))
            {
                return null; // ID inválido
            }

            // Buscar o usuário no banco de dados com base no ID
            User user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return null; // Usuário não encontrado
            }

            string computedHash = ComputeHash(refresh.RefreshToken);

            // Comparar o hash do refresh token armazenado com o fornecido
            if (computedHash != user.HashedRt)
            {
                return null; // Refresh token inválido
            }

            // Gerar novos tokens
            Tokens newTokens = _tokenService.GenerateTokens(user);

            // Atualizar o campo HashedRt com o novo Refresh Token (hash)
            user.HashedRt = ComputeHash(newTokens.RefreshToken);

            await _userRepository.UpdateAsync(user); // Atualiza o usuário
            await _unitOfWork.Commit(); // Salva as mudanças

            return newTokens;
        }

        private string ComputeHash(string token)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(token);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public async Task<Tokens> SignUpAsync(SignUpRequest signUp)
        {
            // Verificar se o e-mail já está cadastrado
            User existingUser = await _userRepository.GetByEmailAsync(signUp.Email);
            if (existingUser != null)
            {
                return null;
            }

            // Gerar o hash da senha
            string passwordHash = HashPassword(signUp.Hash);

            // Criar o novo usuário
            User newUser = new User
            {
                Email = signUp.Email,
                FullName = signUp.FullName,
                Hash = passwordHash,
            };

            await _userRepository.AddAsync(newUser); // Adiciona o novo usuário

            await _unitOfWork.Commit(); // Salva as mudanças

            // Gerar token para o novo usuário
            Tokens newTokens = _tokenService.GenerateTokens(newUser);
            // Atualizar o campo HashedRt com o novo Refresh Token (hash)
            newUser.HashedRt = ComputeHash(newTokens.RefreshToken);

            await _userRepository.UpdateAsync(newUser); // Atualiza o usuário
            
            await _unitOfWork.Commit(); // Salva as mudanças

            return newTokens;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            string hashOfInput = HashPassword(password);
            return hashOfInput == storedHash;
        }
    }
}
