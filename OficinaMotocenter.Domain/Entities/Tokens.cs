namespace OficinaMotocenter.Domain.Entities
{
    /// <summary>
    /// Represents a pair of tokens used for authentication, including both access and refresh tokens.
    /// </summary>
    public class Tokens : BaseEntity
    {
        /// <summary>
        /// Gets or sets the access token, used for authenticating user requests.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token, used for renewing the access token when it expires.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
