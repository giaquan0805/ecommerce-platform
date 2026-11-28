namespace ecommerce_platform.Features.Auth.DTOs
{
    public class AuthResponse
    {
        public string UserId { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? FullName { get; set; }

        public string? AvatarUrl { get; set; }

        public IList<string> Roles { get; set; } = new List<string>();

        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }
    }
}