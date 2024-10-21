namespace OficinaMotocenter.Application.Dto.Requests.Auth
{
    public class SignUpRequest 
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Hash { get; set; }
    }
}
