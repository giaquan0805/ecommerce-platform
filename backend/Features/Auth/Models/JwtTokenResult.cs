namespace ecommerce_platform.Features.Auth.Models
{
    public class JwtTokenResult
    {
        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }
    }
}