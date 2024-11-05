namespace OficinaMotocenter.Application.Dto.Requests.Auth
{
    /// <summary>
    /// DTO for requesting a new access token using a refresh token.
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// Gets or sets the refresh token used to request a new access token.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
