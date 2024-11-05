namespace OficinaMotocenter.Application.Dto.Requests.Auth
{
    /// <summary>
    /// DTO for user sign-in, containing necessary authentication information.
    /// </summary>
    public class SignInRequest
    {
        /// <summary>
        /// Gets or sets the email address of the user attempting to sign in.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the hashed password used to authenticate the user.
        /// </summary>
        public string Hash { get; set; }
    }
}
