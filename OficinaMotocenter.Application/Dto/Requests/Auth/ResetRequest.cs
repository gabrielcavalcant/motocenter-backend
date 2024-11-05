namespace OficinaMotocenter.Application.Dto.Requests.Auth
{
    /// <summary>
    /// DTO for requesting a password reset, containing the user's email.
    /// </summary>
    public class ResetRequest
    {
        /// <summary>
        /// Gets or sets the email address of the user requesting a password reset.
        /// </summary>
        public string Email { get; set; }
    }
}
