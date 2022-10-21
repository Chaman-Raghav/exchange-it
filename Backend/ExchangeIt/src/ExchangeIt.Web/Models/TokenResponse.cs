namespace ExchangeIt.Web.Models
{
    public class TokenResponse
    {
        public string TokenType { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public string ExpiresIn { get; set; }
    }
}
