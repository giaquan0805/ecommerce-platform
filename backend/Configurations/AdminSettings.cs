namespace ecommerce_platform.Configurations
{
    public class AdminSettings
    {
        public const string SectionName = "AdminSettings";

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}