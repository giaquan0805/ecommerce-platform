using System.ComponentModel.DataAnnotations;

namespace ecommerce_platform.Features.Auth.DTOs
{
    public class LoginRequest
    {
        [Required]
        [StringLength(256)]
        public string Identifier { get; set; } = string.Empty;

        [Required]
        [StringLength(128)]
        public string Password { get; set; } = string.Empty;
    }
}
