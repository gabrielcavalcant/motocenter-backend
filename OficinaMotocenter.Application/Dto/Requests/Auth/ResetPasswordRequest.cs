namespace OficinaMotocenter.Application.Dto.Requests.Auth
{
    /// <summary>
    /// DTO for resetting a user's password, containing necessary information for verification and update.
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// Gets or sets the email address of the user requesting a password reset.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the token provided to the user for password reset verification.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the new password that will replace the old password.
        /// </summary>
        public string NewPassword { get; set; }
    }
}
