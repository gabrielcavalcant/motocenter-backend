using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Auth;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotorcycle.API.Controllers

{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IPasswordResetService _passwordResetService;

        public AuthController(IAuthService authService, IPasswordResetService passwordResetService)
        {
            _authService = authService;
            _passwordResetService = passwordResetService; // Inicializando o _passwordResetService
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Login([FromBody] SignInRequest login)
        {
            Tokens token = await _authService.SignInAsync(login);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUp)
        {
            Tokens token = await _authService.SignUpAsync(signUp);
            if (token == null)
            {
                return Conflict(new { message = "Email já está em uso." });
            }
            return Ok(token);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            Tokens token = await _authService.RefreshAsync(refreshTokenRequest);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }


        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] ResetRequest resetRequest)
        {
            bool result = await _passwordResetService.SendPasswordResetTokenAsync(resetRequest.Email);
            if (!result)
            {
                return NotFound(new { message = "Email não encontrado." });
            }

            return Ok(new { message = "Token de reset enviado para o email." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest resetPassword)
        {
            bool result = await _passwordResetService.ResetPasswordAsync(
                resetPassword.Email, resetPassword.Token, resetPassword.NewPassword);

            if (!result)
            {
                return BadRequest(new { message = "Token inválido ou expirado." });
            }

            return Ok(new { message = "Senha redefinida com sucesso." });
        }

    }
} 
