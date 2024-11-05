namespace OficinaMotocenter.Application.Dto.Requests.Auth
{
    /// <summary>
    /// DTO for user sign-up, containing necessary information for registration.
    /// </summary>
    public class SignUpRequest
    {
        /// <summary>
        /// Gets or sets the email address of the new user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the full name of the new user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the hashed password for securing the user's account.
        /// </summary>
        public string Hash { get; set; }
    }
}
