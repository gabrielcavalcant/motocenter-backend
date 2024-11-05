using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Auth;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotorcycle.API.Controllers
{
    /// <summary>
    /// Controller responsible for handling authentication and password management endpoints.
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IPasswordResetService _passwordResetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">Service to handle authentication operations.</param>
        /// <param name="passwordResetService">Service to handle password reset operations.</param>
        public AuthController(IAuthService authService, IPasswordResetService passwordResetService)
        {
            _authService = authService;
            _passwordResetService = passwordResetService;
        }

        /// <summary>
        /// Authenticates a user based on their credentials and returns a token if successful.
        /// </summary>
        /// <param name="login">The user's login information.</param>
        /// <returns>An <see cref="IActionResult"/> containing a token or Unauthorized if authentication fails.</returns>
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

        /// <summary>
        /// Registers a new user and returns a token upon successful registration.
        /// </summary>
        /// <param name="signUp">The user's signup information.</param>
        /// <returns>An <see cref="IActionResult"/> containing a token or Conflict if email is already in use.</returns>
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUp)
        {
            Tokens token = await _authService.SignUpAsync(signUp);
            if (token == null)
            {
                return Conflict(new { message = "Email is already in use." });
            }
            return Ok(token);
        }

        /// <summary>
        /// Refreshes an authentication token using a refresh token.
        /// </summary>
        /// <param name="refreshTokenRequest">The refresh token request information.</param>
        /// <returns>An <see cref="IActionResult"/> containing a new token or Unauthorized if the refresh fails.</returns>
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

        /// <summary>
        /// Sends a password reset token to the user's email.
        /// </summary>
        /// <param name="resetRequest">The request containing the email for password reset.</param>
        /// <returns>An <see cref="IActionResult"/> indicating success or NotFound if the email is not registered.</returns>
        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] ResetRequest resetRequest)
        {
            bool result = await _passwordResetService.SendPasswordResetTokenAsync(resetRequest.Email);
            if (!result)
            {
                return NotFound(new { message = "Email not found." });
            }

            return Ok(new { message = "Reset token sent to email." });
        }

        /// <summary>
        /// Resets the user's password using a reset token and new password.
        /// </summary>
        /// <param name="resetPassword">The request containing email, reset token, and new password.</param>
        /// <returns>An <see cref="IActionResult"/> indicating success or BadRequest if the token is invalid or expired.</returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest resetPassword)
        {
            bool result = await _passwordResetService.ResetPasswordAsync(
                resetPassword.Email, resetPassword.Token, resetPassword.NewPassword);

            if (!result)
            {
                return BadRequest(new { message = "Invalid or expired token." });
            }

            return Ok(new { message = "Password successfully reset." });
        }
    }
}
