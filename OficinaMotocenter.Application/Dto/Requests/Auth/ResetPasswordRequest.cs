namespace OficinaMotocenter.Application.Dto.Requests.Auth
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
