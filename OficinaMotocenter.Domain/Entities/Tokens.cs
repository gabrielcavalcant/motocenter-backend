namespace OficinaMotocenter.Domain.Entities
{
    public class Tokens : BaseEntity
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
